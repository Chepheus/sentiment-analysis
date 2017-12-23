CREATE TABLE IF NOT EXISTS currency_value (
    value_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    value varchar(1000) NOT NULL,
    close_date DATETIME NOT NULL
);
