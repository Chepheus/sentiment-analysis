CREATE TABLE IF NOT EXISTS config (
    config_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    title varchar(100) NOT NULL,
    value varchar(1000) NOT NULL
);
