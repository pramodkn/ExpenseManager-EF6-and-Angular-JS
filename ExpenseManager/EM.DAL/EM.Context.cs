﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EM.DAL
{
    using DomainModel;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class EMEntities : DbContext
    {
        public EMEntities()
            : base("name=EMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}