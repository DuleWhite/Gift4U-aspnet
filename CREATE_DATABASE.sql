CREATE DATABASE Gift4U;

USE Gift4U;

create table users
(
  userid    int(8) unsigned auto_increment,
  username  varchar(255) null,
  userphone varchar(11)  not null,
  primary key (userid, userphone)
)
  engine = InnoDB;

create index userid
  on users (userid);


-- auto-generated definition
create table products
(
  productid                   int(8) unsigned auto_increment
    primary key,
  productimgname              varchar(255)   not null,
  productimgcount             tinyint        not null,
  productimgformat            varchar(255)   not null,
  productname                 varchar(255)   not null,
  productprice                decimal(10, 2) not null,
  productcolor                varchar(255)   null,
  productsize                 varchar(255)   null,
  productdescription          text           not null,
  productinfo                 text           not null,
  productreturn_refeundpolicy text           not null,
  productshippinginfo         text           not null,
  producttag                  varchar(255)   null
)
  engine = InnoDB;


-- auto-generated definition
create table orders
(
  orderid     int(8) unsigned auto_increment
    primary key,
  userid      int(8) unsigned null,
  totalprice  decimal(10, 2)  null,
  shippingto  varchar(255)    null,
  orderdate   datetime        null,
  orderstatus varchar(255)    null,
  constraint orders_ibfk_1
  foreign key (userid) references users (userid)
    on update set null
    on delete set null
)
  engine = InnoDB;

create index userid
  on orders (userid);


-- auto-generated definition
create table order_product
(
  orderid       int(8) unsigned not null,
  productid     int(8) unsigned not null,
  quantity      int(255)        null,
  selectedcolor varchar(255)    not null,
  selectedsize  varchar(255)    not null,
  primary key (orderid, productid, selectedcolor, selectedsize),
  constraint order_product_ibfk_1
  foreign key (orderid) references orders (orderid),
  constraint order_product_ibfk_2
  foreign key (productid) references products (productid)
)
  engine = InnoDB;

create index productid
  on order_product (productid);


INSERT INTO `products` VALUES (10000001, 'product-1', 2, 'jpg', 'Men\'s shoes', 85.00, 'Black,Brown', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Best Seller');
INSERT INTO `products` VALUES (10000002, 'product-2', 2, 'jpg', 'Lipstick', 20.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Recommend');
INSERT INTO `products` VALUES (10000003, 'product-3', 2, 'jpg', 'Men\'s shirts', 10.00, 'Black,White', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Best Seller');
INSERT INTO `products` VALUES (10000004, 'product-4', 2, 'jpg', 'Watch', 25.00, 'Black,Gray,White', 'Small,Medium,Large', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', '');
INSERT INTO `products` VALUES (10000005, 'product-5', 2, 'jpg', 'Hand soap', 7.50, '', 'Small,Large', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', '');
INSERT INTO `products` VALUES (10000006, 'product-6', 2, 'jpg', 'Backpack', 15.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', '');
INSERT INTO `products` VALUES (10000007, 'product-7', 2, 'jpg', 'Women\'s shoes', 85.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Best Seller');
INSERT INTO `products` VALUES (10000008, 'product-8', 2, 'jpg', 'Hair dryer', 40.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'New');
INSERT INTO `products` VALUES (10000009, 'product-9', 2, 'jpg', 'Nail polish', 130.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'New');
INSERT INTO `products` VALUES (10000010, 'product-10', 2, 'jpg', 'Bra', 45.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'New');
INSERT INTO `products` VALUES (10000011, 'product-11', 2, 'jpg', 'Lssuing card', 95.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Best Seller');
INSERT INTO `products` VALUES (10000012, 'product-12', 2, 'jpg', 'Vest', 120.00, '', '', 'I\'m a product description. I\'m a great place to add more details about your product such as sizing, material, care instructions and cleaning instructions.', 'I\'m a product detail. I\'m a great place to add more information about your product such as sizing, material, care and cleaning instructions. This is also a great space to write what makes this product special and how your customers can benefit from this item.', 'I’m a Return and Refund policy. I’m a great place to let your customers know what to do in case they are dissatisfied with their purchase. Having a straightforward refund or exchange policy is a great way to build trust and reassure your customers that they can buy with confidence.', 'I\'m a shipping policy. I\'m a great place to add more information about your shipping methods, packaging and cost. Providing straightforward information about your shipping policy is a great way to build trust and reassure your customers that they can buy from you with confidence.', 'Recommend');






