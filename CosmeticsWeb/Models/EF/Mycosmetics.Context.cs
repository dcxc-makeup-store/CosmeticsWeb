﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CosmeticsWeb.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CosmeticsEntities : DbContext
    {
        public CosmeticsEntities()
            : base("name=CosmeticsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<订单表> 订单表 { get; set; }
        public virtual DbSet<订单明细表> 订单明细表 { get; set; }
        public virtual DbSet<购物车表> 购物车表 { get; set; }
        public virtual DbSet<商品类型表> 商品类型表 { get; set; }
        public virtual DbSet<商品评论表> 商品评论表 { get; set; }
        public virtual DbSet<商品信息表> 商品信息表 { get; set; }
        public virtual DbSet<用户表> 用户表 { get; set; }
        public virtual DbSet<账户记录表> 账户记录表 { get; set; }
    }
}
