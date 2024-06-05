use master
GO
create database Ogani
GO
use Ogani
GO

CREATE TABLE Category (
	cate_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cate_name varchar(100)
)
GO

create table Product (
	product_id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	product_name varchar(100),
	product_desc varchar(100),
	product_price decimal(10, 2),
	product_quantity int,
    product_exp date,
    product_img text,
    product_add date default getdate(),
	cate_id int,
	FOREIGN KEY (cate_id)
	REFERENCES Category (cate_id)
)
GO

create table Customer (
	cus_id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	cus_name varchar(100),
	cus_email varchar(100),
	cus_password varchar(100),
	cus_address varchar(100),
	cus_phone varchar(100),
)
GO

create table Shipment (
	ship_id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ship_date datetime,
	ship_address varchar(100),
	ship_state varchar(20),
	ship_code varchar(10),
	cus_id int,
	FOREIGN KEY (cus_id)
	REFERENCES Customer(cus_id)
)
GO

create table Cart (
	cart_id int NOT NULL IDENTITY(1,1),
	cart_quantity int,
	cus_id int,
	product_id int,
	PRIMARY KEY (cart_id, cus_id),
	FOREIGN KEY (cus_id)
	REFERENCES Customer (cus_id),
	FOREIGN KEY (product_id)
	REFERENCES Product (product_id)
)
GO

create table Payment (
	payment_id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	payment_date datetime,
	payment_method varchar(100),
	payment_amount decimal(10,2),
	cus_id int,
	FOREIGN KEY (cus_id)
	REFERENCES Customer (cus_id)
)
GO

create table Orders (
	order_id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	order_date datetime,
	order_totalprice decimal(10,2),
    order_add date default getdate()
	cus_id int,
	payment_id int,
	ship_id int
	FOREIGN KEY (cus_id)
	REFERENCES Customer (cus_id),
	FOREIGN KEY (payment_id)
	REFERENCES Payment (payment_id),
	FOREIGN KEY (ship_id)
	REFERENCES Shipment (ship_id)
)
GO

create table Order_Item (
	order_item_id int NOT NULL IDENTITY(1,1),
	order_item_quantity int, 
	order_item_price decimal(10,2),
    order_item_add date default getdate(),
	product_id int,
	order_id int, 
	primary key (order_item_id, product_id),
	FOREIGN KEY (product_id)
	REFERENCES Product (product_id),
	FOREIGN KEY (order_id)
	REFERENCES Orders (order_id)
)
GO

create table Wishlist (
	wishlist_id int NOT NULL IDENTITY(1,1),
	cus_id int, 
	product_id int,
	PRIMARY KEY(wishlist_id, cus_id),
	FOREIGN KEY (cus_id)
	REFERENCES Customer (cus_id),
	FOREIGN KEY (product_id)
	REFERENCES Product (product_id)
)
GO