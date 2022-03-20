var mysql = require("mysql");
var con = mysql.createConnection({
    host: process.env.HOST || 'localhost',
    user: process.env.USER || 'root',
    password: process.env.PASSWORD || '',
    database: process.env.DB
});
con.connect();
module.exports = con;