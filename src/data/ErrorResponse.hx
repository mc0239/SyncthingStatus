package data;

typedef Error = {
    var when: String; // TODO some kind of datetime type
    var message: String;
}

typedef ErrorResponse = {
    var errors: Array<Error>;
}