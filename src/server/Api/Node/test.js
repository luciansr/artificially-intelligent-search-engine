neural = require('./neuralNetwork');

async function callback(err, result) {
    console.log(result);
}

async function test() {
    await neural(callback, {
        "teste": 123
    });
}

test();