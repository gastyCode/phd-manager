create table users
(
    user_id      integer                  not null
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

create table theses
(
    thesis_id     integer     not null
        constraint theses_pk
            primary key,
    title         varchar(50) not null,
    description   text,
    student_id    integer
        constraint theses_pk_2
            unique
        constraint theses_users_user_id_fk
            references users,
    supervisor_id integer     not null
        constraint theses_users_user_id_fk_2
            references users,
    opponent_id   integer
        constraint theses_users_user_id_fk_3
            references users,
    year          integer     not null
);

alter table theses
    owner to postgres;

