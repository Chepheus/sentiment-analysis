CREATE TABLE IF NOT EXISTS words (
    word_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    word varchar(255) NOT NULL,
    is_positive SMALLINT(1) NOT NULL
);
