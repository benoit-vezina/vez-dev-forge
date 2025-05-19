using System;
using Microsoft.EntityFrameworkCore;
using PerfectRestApi.Repository.Entities;

namespace PerfectRestApi.Repository;

public class MyContext(DbContextOptions<MyContext> options) : DbContext(options)
{
    public DbSet<Cookie> Cookies { get; set; }
}
