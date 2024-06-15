using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class OganiContext : DbContext
{
    public OganiContext()
    {
    }

    public OganiContext(DbContextOptions<OganiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VIDUCTHIEN\\SQLEXPRESS;Initial Catalog=Ogani;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.CusId }).HasName("PK__Cart__B071FE39E5653E3E");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cart_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.CartQuantity).HasColumnName("cart_quantity");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__cus_id__2F10007B");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__product_id__300424B4");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__Category__34EAD17316D9CB34");

            entity.ToTable("Category");

            entity.Property(e => e.CateId).HasColumnName("cate_id");
            entity.Property(e => e.CateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cate_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId).HasName("PK__Customer__E84D41E81359B2B4");

            entity.ToTable("Customer");

            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.CusAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cus_address");
            entity.Property(e => e.CusEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cus_email");
            entity.Property(e => e.CusName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cus_name");
            entity.Property(e => e.CusPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cus_password");
            entity.Property(e => e.CusPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cus_phone");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__4659622933CFD391");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.OrderAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("order_add");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderTotalprice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("order_totalprice");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Orders__ship_id__36B12243");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Orders__payment___37A5467C");

            entity.HasOne(d => d.Ship).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK__Orders__ship_id__38996AB5");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderItemId, e.ProductId }).HasName("PK__Order_It__73149163CE8BA507");

            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("order_item_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderItemAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("order_item_add");
            entity.Property(e => e.OrderItemPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("order_item_price");
            entity.Property(e => e.OrderItemQuantity).HasColumnName("order_item_quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Ite__order__3D5E1FD2");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Ite__produ__3C69FB99");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EAACB08B5E");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("payment_amount");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Cus).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Payment__cus_id__32E0915F");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF5D304F948");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CateId).HasColumnName("cate_id");
            entity.Property(e => e.ProductAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("product_add");
            entity.Property(e => e.ProductDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_desc");
            entity.Property(e => e.ProductExp).HasColumnName("product_exp");
            entity.Property(e => e.ProductImg)
                .HasColumnType("text")
                .HasColumnName("product_img");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("product_price");
            entity.Property(e => e.ProductQuantity).HasColumnName("product_quantity");
            entity.Property(e => e.ProductSellquantity).HasColumnName("product_sellquantity");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK__Product__cate_id__276EDEB3");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipId).HasName("PK__Shipment__CCF471DA53F3B13D");

            entity.ToTable("Shipment");

            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ship_address");
            entity.Property(e => e.ShipCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ship_code");
            entity.Property(e => e.ShipDate)
                .HasColumnType("datetime")
                .HasColumnName("ship_date");
            entity.Property(e => e.ShipState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ship_state");

            entity.HasOne(d => d.Cus).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Shipment__cus_id__2C3393D0");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => new { e.WishlistId, e.CusId }).HasName("PK__Wishlist__FFD58550D75777F6");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishlistId)
                .ValueGeneratedOnAdd()
                .HasColumnName("wishlist_id");
            entity.Property(e => e.CusId).HasColumnName("cus_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Cus).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.CusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__cus_id__403A8C7D");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist__produc__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
