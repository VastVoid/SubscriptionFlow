﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Common.Configuration.Connection;

namespace Persistence;

public class PostgreDbContext : DbContext
{
    private readonly IConnectionStringFactory _connectionStringBuilder;

    public PostgreDbContext(IConnectionStringFactory connectionStringBuilder)
    {
        _connectionStringBuilder = connectionStringBuilder
            ?? throw new ArgumentNullException($"{nameof(connectionStringBuilder)} is not specified");

        Database.EnsureCreated();
    }

    public DbSet<Subscription> Subscribers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTime>(
            v => v.UtcDateTime,
            v => new DateTimeOffset(v, TimeSpan.Zero));
        
        var nullabeDateTimeOffsetConverter = new ValueConverter<DateTimeOffset?, DateTime?>(
            v => v.HasValue ? v.Value.UtcDateTime : default,
            v => v.HasValue ? new DateTimeOffset(v.Value, TimeSpan.Zero) : default);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ConvertToSnakeCase(property.Name));
                if (property.ClrType == typeof(DateTimeOffset))
                {
                    property.SetValueConverter(dateTimeOffsetConverter);
                }
                if (property.ClrType == typeof(DateTimeOffset?))
                {
                    property.SetValueConverter(nullabeDateTimeOffsetConverter);
                }
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conectionString = _connectionStringBuilder?.Create();
        optionsBuilder.UseNpgsql(conectionString,
            option => option.CommandTimeout(TimeSpan.FromSeconds(10).Seconds));
    }

    private static string ConvertToSnakeCase(string name)
    {
        return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}
