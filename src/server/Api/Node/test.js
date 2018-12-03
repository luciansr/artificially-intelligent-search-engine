neural = require('./fit');

async function callback(err, result) {
    console.log(result);
}

async function test1() {
    await neural(callback, 'queryTeste0', {
        "xs": [[0.800, 0.900, 0.300], [0.700, 0.950, 0.300], [0.850, 0.400, 0.200]],
        "ys": [1, 2, 3]
    });
}

async function test2() {
    await neural(callback, 'queryTeste', {
        "xs": [[0.350], [0.650], [0.100], [0.200], [0.300], [0.400]],
        "ys": [4, 7, 1, 2, 3, 4]
    });
}

test1();