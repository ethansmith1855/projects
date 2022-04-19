const Web3 = require('web3');

const web3 = new Web3('https://mainnet.infura.io/v3/d2361eb14ad5403d988ddaf293d92238');
//Connected to the blockchain

const contract = new web3.eth.Contract(
    abi,
    adress
);