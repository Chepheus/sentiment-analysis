CREATE TABLE IF NOT EXISTS posts (
    post_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    title varchar(1000) NOT NULL,
    href varchar(1000) NOT NULL,
    post_date DATETIME NOT NULL
);
