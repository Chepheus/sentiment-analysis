CREATE TABLE IF NOT EXISTS post_extremum (
    post_extremum_id int(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    post_id int(10) NOT NULL,
    value_id int(10) NOT NULL,
    extremum_id int(1) NOT NULL
);
