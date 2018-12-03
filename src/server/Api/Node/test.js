neural = require('./fit');

async function callback(err, result) {
    console.log(result);
}

async function test() {
    await neural(callback, {
        "xs": [[800, 900, 300], [700, 950, 300], [850, 400, 200]],
        "ys": [1, 2, 3]
    });
}

test();