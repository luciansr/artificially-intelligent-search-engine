
module.exports = async function (callback, query, data) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.

    // Define a model for linear regression.
    const model = tf.sequential();

    const xSize = 4 || data.xs.length;
    const xWidth = 1 || data.xs[0].length || 1;
    const ySize = 4 || data.ys.length;
    const yWidth = 1;

    model.add(tf.layers.dense({ units: yWidth, inputShape: [xWidth] }));

    // Prepare the model for training: Specify the loss and the optimizer.
    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });

    // const xs = tf.tensor2d(data.xs, [xSize, xWidth]);
    // const ys = tf.tensor2d(data.ys, [ySize, yWidth]);

    const xs = tf.tensor2d([0.350, 0.650, 0.100, 0.200], [xSize, xWidth]);
    const ys = tf.tensor2d([3, 6, 1, 2], [ySize, yWidth]);

    // Train the model using the data.
    model.fit(xs, ys, {epochs: 40}).then(() => {
        const savedModel = model.toJSON();

        model.predict(tf.tensor2d([[0.110], [0.810], [0.350]], [3, 1])).print();

        var folderName = query.replace(/[^a-zA-Z0-9]/gi, '');

        model.save('file://./LearnedWeights/' + folderName).then(saved => {
            callback(null, JSON.stringify(savedModel));
        });
    });
    
} 