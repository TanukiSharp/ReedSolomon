using System;

namespace ReedSolomon
{
    public enum ErrorCorrectionCodeType
    {
        QRCode,
        DataMatrix
    }

    public static class ReedSolomonAlgorithm
    {
        /// <summary>
        /// Produces error correction codewords for a message using the Reed-Solomon algorithm.
        /// </summary>
        /// <param name="message">The message to compute the error correction codewords.</param>
        /// <param name="eccCount">The number of error correction codewords desired.</param>
        /// <param name="eccType">The type of Galois field to use to encode error correction codewords.</param>
        /// <returns>Returns the computed error correction codewords.</returns>
        public static byte[] Encode(byte[] message, int eccCount, ErrorCorrectionCodeType eccType)
        {
            GenericGF galoisField;

            if (eccType == ErrorCorrectionCodeType.QRCode)
                galoisField = GenericGF.QR_CODE_FIELD_256;
            else if (eccType == ErrorCorrectionCodeType.DataMatrix)
                galoisField = GenericGF.DATA_MATRIX_FIELD_256;
            else
                throw new ArgumentException($"Invalid '{nameof(eccType)}' argument.", nameof(eccType));

            var reedSolomonEncoder = new ReedSolomonEncoder(galoisField);
            return reedSolomonEncoder.EncodeEx(message, eccCount);
        }

        /// <summary>
        /// Produces error correction codewords for a message using the Reed-Solomon algorithm.
        /// </summary>
        /// <param name="message">The message to compute the error correction codewords.</param>
        /// <param name="eccCount">The number of error correction codewords desired.</param>
        /// <returns>Returns the computed error correction codewords.</returns>
        public static byte[] Encode(byte[] message, int eccCount)
        {
            return Encode(message, eccCount, ErrorCorrectionCodeType.DataMatrix);
        }

        /// <summary>
        /// Repairs a possibly broken message using the Reed-Solomon algorithm.
        /// </summary>
        /// <param name="message">The possibly broken message to repair.</param>
        /// <param name="ecc">The available error correction codewords.</param>
        /// <param name="eccType">The type of Galois field to use to decode message.</param>
        /// <returns>Returns the repaired message, or null if it cannot be repaired.</returns>
        public static byte[] Decode(byte[] message, byte[] ecc, ErrorCorrectionCodeType eccType)
        {
            GenericGF galoisField;

            if (eccType == ErrorCorrectionCodeType.QRCode)
                galoisField = GenericGF.QR_CODE_FIELD_256;
            else if (eccType == ErrorCorrectionCodeType.DataMatrix)
                galoisField = GenericGF.DATA_MATRIX_FIELD_256;
            else
                throw new ArgumentException($"Invalid '{nameof(eccType)}' argument.", nameof(eccType));

            var reedSolomonDecoder = new ReedSolomonDecoder(galoisField);
            return reedSolomonDecoder.DecodeEx(message, ecc);
        }

        /// <summary>
        /// Repairs a possibly broken message using the Reed-Solomon algorithm.
        /// </summary>
        /// <param name="message">The possibly broken message to repair.</param>
        /// <param name="ecc">The available error correction codewords.</param>
        /// <returns>Returns the repaired message, or null if it cannot be repaired.</returns>
        public static byte[] Decode(byte[] message, byte[] ecc)
        {
            return Decode(message, ecc, ErrorCorrectionCodeType.DataMatrix);
        }
    }
}
