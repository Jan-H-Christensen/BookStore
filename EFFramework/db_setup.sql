-- Create the database if it does not exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BookStoreDB')
BEGIN
    CREATE DATABASE BookStoreDB;
END;

-- Switch to the BookStoreDB database
USE BookStoreDB;

-- Drop tables if they already exist to avoid conflicts
DROP TABLE IF EXISTS OrderDetails, Orders, Inventory, Books, Authors, Customers;

-- Create Authors Table
CREATE TABLE Authors (
    author_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    country VARCHAR(255) NOT NULL,
    date_of_birth DATE NOT NULL
);

-- Create Books Table
CREATE TABLE Books (
    book_id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    genre VARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    stock_level INT NOT NULL,
    author_id INT,
    FOREIGN KEY (author_id) REFERENCES Authors(author_id)
);

-- Create Customers Table
CREATE TABLE Costumers (
    costumers_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    address TEXT NOT NULL,
    phone VARCHAR(50)
);

-- Create Orders Table
CREATE TABLE Orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    costumers_id INT,
    order_date DATETIME NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (costumers_id) REFERENCES Costumers(costumers_id)
);

-- Create OrderDetails Table
CREATE TABLE OrderDetails (
    order_detail_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    book_id INT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (book_id) REFERENCES Books(book_id)
);

-- Create Inventory Table
CREATE TABLE Inventory (
    book_id INT PRIMARY KEY,
    stock_level INT NOT NULL,
    last_updated DATETIME NOT NULL,
    FOREIGN KEY (book_id) REFERENCES Books(book_id)
);

-- Insert into Authors
INSERT INTO Authors (name, country, date_of_birth) VALUES
('Lawrence Brown', 'British Indian Ocean Territory', '1989-10-09'),
('Paul Williams', 'Palau', '2008-02-09'),
('Jeffrey Horton', 'Gambia', '2001-03-03'),
('Steven French', 'Zimbabwe', '1972-09-22'),
('Morgan Holland', 'Netherlands', '1912-05-18');

-- Insert into Books
INSERT INTO Books (title, genre, price, stock_level, author_id) VALUES
('Offer campaign people guess civil.', 'Fiction', 19.31, 99, 1),
('Industry project fish least.', 'Romance', 25.05, 17, 1),
('International about.', 'Non-Fiction', 39.85, 9, 1),
('Employee market attention light.', 'Non-Fiction', 22.37, 91, 2),
('Hospital worry conference bank.', 'Non-Fiction', 12.49, 70, 5),
('Start worry rather cause bank.', 'Romance', 41.32, 35, 4),
('Military sell.', 'Romance', 33.91, 71, 4),
('Agency choose friend.', 'Romance', 49.13, 93, 4),
('Start country form allow.', 'Fiction', 38.56, 78, 4),
('Thank hope base even health.', 'Science Fiction', 19.03, 100, 3);

-- Insert into Customers
INSERT INTO Costumers (name, email, address, phone) VALUES
('Kimberly Nichols', 'cmartinez@santiago-medina.com', '0757 Sarah Pines Suite 262\nMichaelmouth, IL 60035', '+1-237-592-9664x11363'),
('Michael Beck', 'taylorlaurie@rodriguez.com', '5973 Darius Common Suite 597\nNew Adam, PA 26968', '(204)140-8526'),
('Robert Page', 'susan28@robertson-smith.biz', 'PSC 4049, Box 1769\nAPO AE 83942', '635.029.4044'),
('Lisa Nelson', 'warrendaniel@garcia-baker.com', '5657 Brown Ville Apt. 833\nEast Ashley, MI 22000', '110.435.2184x564'),
('Alice Estrada', 'tdunlap@ramsey.com', '51797 Amanda Plaza\nEast Brian, NM 22021', '+1-369-086-3449x44578');

-- Insert into Orders
INSERT INTO Orders (costumers_id, order_date, total_amount) VALUES
(4, '2024-11-15 06:56:37', 120.86),
(5, '2024-08-29 10:27:22', 57.09),
(5, '2024-02-11 19:52:42', 49.13),
(4, '2024-11-12 19:52:50', 347.65),
(2, '2024-10-15 07:09:44', 169.92),
(4, '2024-02-09 16:54:39', 96.55),
(1, '2024-10-05 18:32:54', 280.30),
(3, '2024-04-15 03:19:13', 82.64),
(1, '2024-11-03 08:55:57', 306.66),
(2, '2024-01-22 03:22:32', 86.06);

-- Insert into OrderDetails
INSERT INTO OrderDetails (order_id, book_id, quantity, price) VALUES
(1, 4, 2, 22.37),
(1, 10, 4, 19.03),
(2, 10, 3, 19.03),
(3, 8, 1, 49.13),
(4, 9, 4, 38.56),
(4, 8, 2, 49.13),
(4, 10, 5, 19.03),
(5, 9, 2, 38.56),
(5, 7, 2, 33.91),
(5, 5, 2, 12.49);

-- Insert into Inventory
INSERT INTO Inventory (book_id, stock_level, last_updated) VALUES
(1, 99, '2024-08-04 17:46:14'),
(2, 17, '2024-07-28 15:20:21'),
(3, 9, '2024-01-09 19:04:31'),
(4, 91, '2024-11-14 02:01:25'),
(5, 70, '2024-04-12 23:55:28'),
(6, 35, '2024-11-03 10:37:16'),
(7, 71, '2024-06-01 17:42:36'),
(8, 93, '2024-01-15 07:46:00'),
(9, 78, '2024-05-06 12:30:28'),
(10, 100, '2024-11-17 17:01:15');
