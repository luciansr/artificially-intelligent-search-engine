
module.exports = async function (callback, query, data) {
    const tf = require('@tensorflow/tfjs');
    // Load the binding:
    require('@tensorflow/tfjs-node');  // Use '@tensorflow/tfjs-node-gpu' if running with GPU.



    const xSize = data.xs.length;
    const xWidth = data.xs[0].length || 1;
    const ySize = data.ys.length;
    const yWidth = 1;

    // Define a model for linear regression.
    // Define input, which has a size of 5 (not including batch dimension).
    const input = tf.input({shape: [xWidth]});

    // Second dense layer uses softmax activation.
    const denseLayer2 = tf.layers.dense({units: 1, activation: 'softmax'});

    // Obtain the output symbolic tensor by applying the layers on the input.
    const output = denseLayer2.apply(input);

    // Create the model based on the inputs.
    const model = tf.model({inputs: input, outputs: output});

    model.compile({ loss: 'meanSquaredError', optimizer: 'sgd' });

    const xs = tf.tensor2d(data.xs, [xSize, xWidth]);
    const ys = tf.tensor2d(data.ys, [ySize, yWidth]);

    // Train the model using the data.
    model.fit(xs, ys, {epochs: 40}).then(() => {
        const savedModel = model.toJSON();

        model.predict(tf.tensor2d([110, 810, 350], [3, 1])).print();

        var folderName = query.replace(/[^a-zA-Z0-9]/gi, '');

        model.save('file://./LearnedWeights/' + folderName).then(saved => {
            callback(null, JSON.stringify(savedModel));
        });
    });
    
} 