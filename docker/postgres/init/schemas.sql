create table users
(
    id           integer                  not null
        primary key,
    username     varchar(20)              not null
        unique,
    display_name varchar(50)              not null,
    first_name   varchar(20)              not null,
    last_name    varchar(30)              not null,
    role         varchar(10) default USER not null,
    first_login  timestamp                not null
);

alter table users
    owner to postgres;