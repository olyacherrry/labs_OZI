using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ECB
{
    enum Permutations { P10, P8, P4, IP, E_P, IP_1 }

    class SDES
    {
        internal static StringBuilder log = new StringBuilder();
        internal static Int32[,] s_l = new Int32[,]
        {
            { 1, 0, 3, 2 },
            { 3, 2, 1, 0 },
            { 0, 2, 1, 3 },
            { 3, 1, 3, 1 },
        };
        internal static Int32[,] s_r = new Int32[,]
        {
            { 1, 1, 2, 3 },
            { 2, 0, 1, 3 },
            { 3, 0, 1, 0 },
            { 2, 1, 0, 3 },
        };
        public static Tuple<BitArray, String> MakeS_DES(BitArray textBits, String keyText, String nunceText, Int32 counter, Boolean type, Boolean mode)
        {
            log.Clear();
            BitArray bitArray = new BitArray(8);
            if (!mode)
                bitArray = NoneS_DES(textBits, keyText, type);
            else
                bitArray = CTR_S_DES(textBits, keyText, nunceText ,counter);
            return Tuple.Create(bitArray, log.ToString());
        }

        // Преобразование данных и расчет ключей
        public static BitArray NoneS_DES(BitArray textBits, String keyText, Boolean type)
        {
            log.Append("----------------Keys----------------").Append(Environment.NewLine);
            log.Append(String.Format("{0, -5} : {1}", "KEY", keyText)).Append(Environment.NewLine);
            BitArray keyBit = ToBitArray(Int32.Parse(keyText), 10);
            log.Append(String.Format("{0, -5} : {1}", "KEY BIT", ToString(keyBit))).Append(Environment.NewLine);
            BitArray key = RefreshArray(keyBit, Permutations.P10);
            log.Append(String.Format("{0, -5} : {1}", "P10", ToString(key))).Append(Environment.NewLine);
            MoveLeft(key, 1);
            log.Append(String.Format("{0, -5} : {1}", "LS1", ToString(key))).Append(Environment.NewLine);
            BitArray k1 = RefreshArray(key, Permutations.P8);
            log.Append(String.Format("{0, -5} : {1}", "KEY1(P8)", ToString(k1))).Append(Environment.NewLine);
            MoveLeft(key, 2);
            log.Append(String.Format("{0, -5} : {1}", "LS2", ToString(key))).Append(Environment.NewLine);
            BitArray k2 = RefreshArray(key, Permutations.P8);
            log.Append(String.Format("{0, -5} : {1}", "KEY2(P8)", ToString(k2))).Append(Environment.NewLine).Append(Environment.NewLine);
            if (!type)
            {
                BitArray buf = new BitArray(k1);
                k1 = new BitArray(k2);
                k2 = new BitArray(buf);
            }
            log.Append("----------------S-DES----------------").Append(Environment.NewLine);
            log.Append(String.Format("{0, -5} : {1}", "Plain text", ToString(textBits))).Append(Environment.NewLine);
            BitArray bitArray = MakeTransformation(textBits, k1, k2);
            return bitArray;
        }

        // Преобразование данных и расчет ключей
        public static BitArray CTR_S_DES(BitArray textBits, String keyText, String nunceText, Int32 counter)
        {
            log.Append("----------------Keys----------------").Append(Environment.NewLine);
            log.Append(String.Format("{0, -5} : {1}", "KEY", keyText)).Append(Environment.NewLine);
            BitArray keyBit = ToBitArray(Int32.Parse(keyText), 10);
            log.Append(String.Format("{0, -5} : {1}", "KEY BIT", ToString(keyBit))).Append(Environment.NewLine);
            BitArray key = RefreshArray(keyBit, Permutations.P10);
            log.Append(String.Format("{0, -5} : {1}", "P10", ToString(key))).Append(Environment.NewLine);
            MoveLeft(key, 1);
            log.Append(String.Format("{0, -5} : {1}", "LS1", ToString(key))).Append(Environment.NewLine);
            BitArray k1 = RefreshArray(key, Permutations.P8);
            log.Append(String.Format("{0, -5} : {1}", "KEY1(P8)", ToString(k1))).Append(Environment.NewLine);
            MoveLeft(key, 2);
            log.Append(String.Format("{0, -5} : {1}", "LS2", ToString(key))).Append(Environment.NewLine);
            BitArray k2 = RefreshArray(key, Permutations.P8);
            log.Append(String.Format("{0, -5} : {1}", "KEY2(P8)", ToString(k2))).Append(Environment.NewLine).Append(Environment.NewLine);



            log.Append("----------------S-DES-CTR------------").Append(Environment.NewLine);

            BitArray counterBit = ToBitArray(counter + Int32.Parse(nunceText), 8);
            log.Append(String.Format("{0, -5} : {1}", "Counter text", ToString(counterBit))).Append(Environment.NewLine);

            BitArray bitArray = MakeTransformation(counterBit, k1, k2);
            log.Append(String.Format("{0, -5} : {1}", "Plain text", ToString(bitArray))).Append(Environment.NewLine);
            
            log.Append("----------------S-DES-XOR------------").Append(Environment.NewLine);
            BitArray result = bitArray.Xor(textBits);
            log.Append(String.Format("{0, -5} : {1}", "XOR", ToString(result))).Append(Environment.NewLine);

            return result;
        }

        // Общая последовательность S-DES
        private static BitArray MakeTransformation(BitArray openTextBitArray, BitArray key1, BitArray key2)
        {
            BitArray ip = RefreshArray(openTextBitArray, Permutations.IP);
            log.Append(String.Format("{0, -5} : {1}", "IP", ToString(ip))).Append(Environment.NewLine);
            log.Append("---Round1---").Append(Environment.NewLine);
            BitArray round1 = MakeRound(ip, key1);
            log.Append(String.Format("{0, -5} : {1}", "After round1", ToString(round1))).Append(Environment.NewLine);
            SwapParts(round1);
            log.Append(String.Format("{0, -5} : {1}", "SW", ToString(round1))).Append(Environment.NewLine);
            log.Append("---Round2---").Append(Environment.NewLine);
            BitArray round2 = MakeRound(round1, key2);
            log.Append(String.Format("{0, -5} : {1}", "After round2", ToString(round2))).Append(Environment.NewLine);
            BitArray ip_1 = RefreshArray(round2, Permutations.IP_1);
            log.Append(String.Format("{0, -5} : {1}", "IP_1", ToString(ip_1))).Append(Environment.NewLine).Append(Environment.NewLine);
            return ip_1;
        }

        // Функция раунда
        private static BitArray MakeRound(BitArray blockBitArray, BitArray key)
        {
            Tuple<BitArray, BitArray> tuple = Divide(blockBitArray);
            BitArray leftPart = tuple.Item1;
            BitArray rightPart = tuple.Item2;
            log.Append(String.Format("{0, -5} : {1}", "L", ToString(leftPart))).Append(Environment.NewLine);
            log.Append(String.Format("{0, -5} : {1}", "R", ToString(rightPart))).Append(Environment.NewLine);
            BitArray e_p = RefreshArray(rightPart, Permutations.E_P);
            log.Append(String.Format("{0, -5} : {1}", "E/P", ToString(e_p))).Append(Environment.NewLine);
            BitArray _ = e_p.Xor(key);
            log.Append(String.Format("{0, -5} : {1}", "XOR(E/P,KEY)", ToString(_))).Append(Environment.NewLine);
            tuple = Divide(_);
            BitArray coLeft = MakeS_Matrix(tuple.Item1, s_l);
            log.Append(String.Format("{0, -5} : {1}", "SL", ToString(coLeft))).Append(Environment.NewLine);
            BitArray coRight = MakeS_Matrix(tuple.Item2, s_r);
            log.Append(String.Format("{0, -5} : {1}", "SR", ToString(coRight))).Append(Environment.NewLine);
            BitArray afterP4 = RefreshArray(Join(coLeft, coRight), Permutations.P4);
            log.Append(String.Format("{0, -5} : {1}", "P4", ToString(afterP4))).Append(Environment.NewLine);
            _ = leftPart.Xor(afterP4);
            log.Append(String.Format("{0, -5} : {1}", "XOR(L,P4)", ToString(_))).Append(Environment.NewLine);
            BitArray out_ = Join(_, rightPart);
            return out_;
        }

        // Преобразование числа в массив битов
        public static BitArray ToBitArray(Int32 number, Int32 size)
        {
            String bitArrayStr = Convert.ToString(number, 2).PadLeft(size, '0');
            BitArray arrayBit = new BitArray(size);
            for (int i = 0; i < bitArrayStr.Length; i++)
            {
                arrayBit.Set(i, bitArrayStr[i].Equals('1') ? true : false);
            }
            return arrayBit;
        }

        // Выбор последовательности для перестановки
        private static BitArray RefreshArray(BitArray bitArray, Permutations permutation)
        {
            Int32[] sequence = new Int32[] { };
            switch (permutation)
            {
                case Permutations.P10:
                    {
                        sequence = new Int32[] { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
                        break;
                    }
                case Permutations.P8:
                    {
                        sequence = new Int32[] { 6, 3, 7, 4, 8, 5, 10, 9 };
                        break;
                    }
                case Permutations.IP:
                    {
                        sequence = new Int32[] { 2, 6, 3, 1, 4, 8, 5, 7 };
                        break;
                    }

                case Permutations.E_P:
                    {
                        sequence = new Int32[] { 4, 1, 2, 3, 2, 3, 4, 1 };
                        break;
                    }
                case Permutations.P4:
                    {
                        sequence = new Int32[] { 2, 4, 3, 1 };
                        break;
                    }
                case Permutations.IP_1:
                    {
                        sequence = new Int32[] { 4, 1, 3, 5, 7, 2, 8, 6 };
                        break;
                    }
            }
            return _RefreshArray(bitArray, sequence);
        }

        // Перестановки в массиве
        private static BitArray _RefreshArray(BitArray bitArray, Int32[] sequence)
        {
            BitArray buf = new BitArray(sequence.Length);
            for (int i = 0; i < sequence.Length; i++)
            {
                buf.Set(i, bitArray.Get(sequence[i] - 1));
            }
            return new BitArray(buf);
        }

        // Сдвиг влево на количество позиций
        public static void MoveLeft(BitArray bitArray, Int32 position)
        {
            for (Int32 i = 0; i < position; i++)
            {
                Int32 center = bitArray.Length / 2;
                Boolean bufLeft = bitArray.Get(0), bufRight = bitArray.Get(center);
                for (Int32 j = 1; j < center; j++)
                {
                    bitArray.Set(j - 1, bitArray.Get(j));
                    bitArray.Set(j - 1 + center, bitArray.Get(j + center));
                }
                bitArray.Set(center - 1, bufLeft);
                bitArray.Set(center * 2 - 1, bufRight);
            }
        }

        // Деление массива на две равные части
        public static Tuple<BitArray, BitArray> Divide(BitArray bitArray)
        {
            Int32 newLength = bitArray.Length / 2;
            BitArray leftPart = new BitArray(newLength);
            BitArray rightPart = new BitArray(newLength);
            for (Int32 i = 0; i < newLength; i++)
            {
                leftPart.Set(i, bitArray.Get(i));
                rightPart.Set(i, bitArray.Get(i + newLength));
            }
            return Tuple.Create(leftPart, rightPart);
        }

        // S-матрица
        public static BitArray MakeS_Matrix(BitArray bitArray, Int32[,] table)
        {
            Int32 row = Convert.ToInt32(bitArray.Get(0)) * 2 + Convert.ToInt32(bitArray.Get(3));
            Int32 column = Convert.ToInt32(bitArray.Get(1)) * 2 + Convert.ToInt32(bitArray.Get(2));
            Int32 number = table[row, column];
            return ToBitArray(number, 2);
        }

        // Объединение двух массивов
        public static BitArray Join(BitArray bitArray1, BitArray bitArray2)
        {
            Int32 size1 = bitArray1.Length;
            Int32 size2 = bitArray2.Length;
            BitArray newArray = new BitArray(size1 + size2);
            for (int i = 0; i < size1; i++)
            {
                newArray.Set(i, bitArray1.Get(i));
            }
            for (int i = 0; i < size2; i++)
            {
                newArray.Set(i + size1, bitArray2.Get(i));
            }
            return newArray;
        }

        // Перестановка левой и правой части массива
        private static void SwapParts(BitArray bitArray)
        {
            int center = bitArray.Length / 2;
            for (int i = 0; i < center; i++)
            {
                Boolean buf = bitArray.Get(i);
                bitArray.Set(i, bitArray.Get(i + center));
                bitArray.Set(i + center, buf);
            }
        }


        public static BitArray Reverse(BitArray bitArray)
        {
            BitArray reversedBitArray = new BitArray(bitArray.Length);
            for(int i = 0; i < bitArray.Length; i++)
            {
                reversedBitArray.Set(i, bitArray.Get(bitArray.Length - i - 1));
            }
            return reversedBitArray;
        }

        public static String ToString(List<BitArray> bitArrayList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (BitArray bitArray in bitArrayList)
            {
                foreach (Boolean b in bitArray)
                {
                    sb.Append(b ? 1 : 0);
                }
            }

            return sb.ToString();
        }

        public static String ToString(BitArray bitArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Boolean b in bitArray)
            {
                sb.Append(b ? 1 : 0);
            }
            return sb.ToString();
        }
    }
}
