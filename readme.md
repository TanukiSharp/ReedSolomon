# Overview

This is a .NET implementation of the Reed-Solomon algorithm, supporting `.NET Standard 1.0`.

My work was mostly and turn an existing mathematician-friendly implementation into a developer-friendly one.
Also, this implementation focuses on a specific case where the Galois field order is precisely the amount of values a `byte` can contain.

For exemple, encoding and decoding methods were taking integers as a series of constants of a polynomial expression, whereas most developers just prefer to work with a `byte[]` representing concrete data. (file content, datagram received from network, etc...)

Even if you are only interested in the developer aspect of this library, I recommend having a look at the following topics:

- Galois fields (or finite fields):
  - https://en.wikipedia.org/wiki/Finite_field
  - http://mathworld.wolfram.com/FiniteField.html
- Reed-Solom:
  - https://en.wikipedia.org/wiki/Reed%E2%80%93Solomon_error_correction
  - https://www.cs.cmu.edu/~guyb/realworld/reedsolomon/reed_solomon_codes.html

# Usage

The Reed-Solomon algorithm is used in various applications, like CD/DVD data storage, QR Codes or bitcoin printed wallets, to avoid a minor loss of information to corrupt the whole data.

The way to use is to encode a message, which produces error correction codewords.

Then, decode the original message providing the original message itself and computed error correction codewords.

Both the original message and the error correction codewords can be damaged, and still allow decoding of the original message.
The amount of acceptable damage depends on the amound of error correction codewords requested when encoding.

# Credits

This library is based on the work of the [ZXing project](https://github.com/zxing/zxing).

Most source files also mention the work of William Rucklidge for his C++ implementation of the Reed-Solomon algorithm.

# License

This project uses source files which are under Apache License 2.0, thus this repository is also under this license.
