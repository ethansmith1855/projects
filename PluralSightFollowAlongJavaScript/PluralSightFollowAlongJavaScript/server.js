'use strict';
var http = require('http');
var fs = require('fs');
var port = process.env.PORT || 1337;

http.createServer(function (req, res) {
    fs.readFile('MainFile.html', function (err, data) {
        res.writeHead(200, { 'Content-Type': 'text/html' });
        res.write(data);
        return res.end();
    });
}).listen(port);
