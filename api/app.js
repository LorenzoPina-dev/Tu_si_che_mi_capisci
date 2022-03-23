require("dotenv").config();
var createError = require("http-errors");
var express = require("express");
var path = require("path");
var cookieParser = require("cookie-parser");
var logger = require("morgan");
var db = require("./util/db");

var indexRouter = require("./routes/index");
var usersRouter = require("./routes/users");
var devicesRouter = require("./routes/devices");
var emotionsRouter = require("./routes/emotions");
var facesRouter = require("./routes/faces");
var skillsRouter = require("./routes/skills");
const { runInNewContext } = require("vm");
var app = express();


// view engine setup
/*app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');*/

app.use(logger("dev"));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());

//app.use(express.static(path.join(__dirname, 'public')));


app.use("/:key/emozioni", controllaUtente, emotionsRouter);
app.use("/:key/volto", controllaUtente, facesRouter);
app.use("/:key/dispositivi", controllaUtente, devicesRouter);
app.use("/:key/skill", controllaUtente, skillsRouter);
app.use("/:key/utente", controllaUtente, usersRouter);
app.use("/", indexRouter);
// catch 404 and forward to error handler
app.use(function(req, res, next) {
    next(createError(404));
});
// error handler
app.use(function(err, req, res, next) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get("env") === "development" ? err : {};

    // render the error page
    res.status(err.status || 500);
    res.header("Content-type", "application/json");
    res.end("{'success':false, 'message':'richiesta non trovata'}");
});

function controllaUtente(req, res, next) {
    console.log(req.params)
    db.query(
        "Select Id,Username,Email,Immagine,Xp from utente where ApiKey=?", [req.params.key],
        (err, result) => {
            if (err) throw err;
            req.Utente = result[0];
            next();
        }
    );
}
module.exports = app;