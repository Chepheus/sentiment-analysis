CREATE TABLE IF NOT EXISTS post_words (
    post_word_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    post_id int(10) NOT NULL,
    word_id int(10) NOT NULL
);
