﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
namespace Fun
{
    public class Import
    {
        /// <summary>
        /// because i did not want to hand write all of the values for this song i instead had chat gpt help me build an importer so i could convert a clone hero chart into usable information
        /// </summary>
        /// <returns></returns>
        public List<Tuple<double, int>> LoadDataFromFile()
        {
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");

                string y = wow;
                // Read all lines from the file
                //string[] lines = File.ReadAllLines(filename);
                string[] lines = y.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


                // Fill the test array with the first 8 lines, or fewer if there are not enough lines
                for (int i = 0; i < lines.Length - 10; ++i)
                {
                lines[i] = lines[i].Trim(); // Store each line, trimming whitespace
                }
                List<Tuple<double, int, int>> parsedData = new List<Tuple<double, int, int>>();
                string pattern = @"(\d+)\s*=\s*N\s*(\d+)\s*(\d+)";

                double oldBPM = 130;
                double newBPM = 120;
                double conversionFactor = oldBPM / newBPM;

                double previousNoteTime = -1; // Initialize to an invalid time

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        double firstNumber = double.Parse(match.Groups[1].Value);
                        int secondNumber = int.Parse(match.Groups[2].Value);
                        int lastNumber = int.Parse(match.Groups[3].Value);

                        // Apply the conversion factor
                        firstNumber *= conversionFactor; // Convert timing
                        lastNumber = (int)(lastNumber * conversionFactor); // Convert hold length

                        // Calculate the time difference
                        double timeDifference = 0;

                        if (previousNoteTime >= 0) // If there is a previous note time
                        {
                            timeDifference = (firstNumber - previousNoteTime) / 1000.0; // Convert milliseconds to seconds
                        }

                        // Update previousNoteTime to the current firstNumber
                        previousNoteTime = firstNumber;

                    // Only add the note if it meets the conditions
                    if (secondNumber != 4) {
                            if (secondNumber != 6) {
                                parsedData.Add(Tuple.Create(timeDifference, secondNumber, lastNumber));
                            }
                        }
                    }
                }
                List<Tuple<double, int>> eee = new List<Tuple<double, int>>();

                foreach (var x in parsedData) {
                    eee.Add(new Tuple<double, int>(x.Item1 * 2.4038, x.Item2));
                }
                return eee;

        }
        public string wow = @"576 = N 1 0
  576 = N 2 0
  576 = N 6 0
  1056 = N 1 48
  1056 = N 2 48
  1056 = N 6 0
  1200 = N 1 96
  1200 = N 2 96
  1200 = N 6 0
  1344 = N 0 0
  1344 = N 2 0
  1344 = N 6 0
  1824 = N 0 48
  1824 = N 2 48
  1824 = N 6 0
  1968 = N 0 144
  1968 = N 2 144
  1968 = N 6 0
  2112 = N 1 384
  2112 = N 3 384
  2112 = N 6 0
  2592 = N 1 48
  2592 = N 3 48
  2592 = N 6 0
  2736 = N 1 24
  2736 = N 3 24
  2736 = N 6 0
  2880 = N 0 384
  2880 = N 1 384
  2880 = N 6 0
  3360 = N 0 48
  3360 = N 1 48
  3360 = N 6 0
  3504 = N 0 0
  3504 = N 1 0
  3504 = N 6 0
  3648 = N 1 0
  3648 = N 2 0
  3648 = N 6 0
  4128 = N 1 48
  4128 = N 2 48
  4128 = N 6 0
  4272 = N 1 96
  4272 = N 2 96
  4272 = N 6 0
  4416 = N 0 0
  4416 = N 2 0
  4416 = N 6 0
  4896 = N 0 48
  4896 = N 2 48
  4896 = N 6 0
  5040 = N 0 144
  5040 = N 2 144
  5040 = N 6 0
  5184 = N 1 384
  5184 = N 3 384
  5184 = N 6 0
  5664 = N 1 48
  5664 = N 3 48
  5664 = N 6 0
  5808 = N 1 24
  5808 = N 3 24
  5808 = N 6 0
  5952 = N 0 384
  5952 = N 1 384
  5952 = N 6 0
  6432 = N 0 48
  6432 = N 1 48
  6432 = N 6 0
  6576 = N 0 0
  6576 = N 1 0
  6576 = N 6 0
  6720 = N 1 0
  6720 = N 2 0
  6720 = N 6 0
  7200 = N 1 48
  7200 = N 3 48
  7200 = N 6 0
  7344 = N 1 96
  7344 = N 2 96
  7344 = N 6 0
  7488 = N 0 0
  7488 = N 2 0
  7488 = N 6 0
  7968 = N 0 48
  7968 = N 2 48
  7968 = N 6 0
  8112 = N 0 144
  8112 = N 2 144
  8112 = N 6 0
  8256 = N 1 384
  8256 = N 3 384
  8256 = N 6 0
  8736 = N 1 48
  8736 = N 3 48
  8736 = N 6 0
  8880 = N 1 24
  8880 = N 3 24
  8880 = N 6 0
  9024 = N 0 384
  9024 = N 1 384
  9024 = N 6 0
  9504 = N 0 48
  9504 = N 6 0
  9648 = N 0 0
  9648 = N 1 0
  9648 = N 6 0
  9792 = N 1 0
  9792 = N 2 0
  9792 = N 6 0
  10272 = N 1 48
  10272 = N 2 48
  10272 = N 6 0
  10416 = N 1 96
  10416 = N 3 96
  10416 = N 6 0
  10560 = N 2 0
  10560 = N 3 0
  10560 = N 6 0
  11040 = N 2 24
  11040 = N 3 24
  11040 = N 6 0
  11184 = N 3 144
  11184 = N 6 0
  11328 = N 1 384
  11328 = N 3 384
  11328 = N 6 0
  11808 = N 1 48
  11808 = N 3 48
  11808 = N 6 0
  11952 = N 1 24
  11952 = N 2 24
  11952 = N 6 0
  12096 = N 0 384
  12096 = N 1 384
  12096 = N 6 0
  12576 = N 0 48
  12576 = N 1 48
  12576 = N 6 0
  12720 = N 0 0
  12720 = N 2 0
  12720 = N 6 0
  12864 = N 1 0
  12864 = N 2 0
  12864 = N 6 0
  13344 = N 1 48
  13344 = N 2 48
  13344 = N 6 0
  13488 = N 1 96
  13488 = N 2 96
  13488 = N 6 0
  13632 = N 0 0
  13632 = N 2 0
  13632 = N 6 0
  14112 = N 0 48
  14112 = N 2 48
  14112 = N 6 0
  14256 = N 0 144
  14256 = N 2 144
  14256 = N 6 0
  14400 = N 1 384
  14400 = N 3 384
  14400 = N 6 0
  14880 = N 1 48
  14880 = N 3 48
  14880 = N 6 0
  15024 = N 1 24
  15024 = N 3 24
  15024 = N 6 0
  15168 = N 0 384
  15168 = N 1 384
  15168 = N 6 0
  15648 = N 0 48
  15648 = N 1 48
  15648 = N 6 0
  15792 = N 0 0
  15792 = N 1 0
  15792 = N 6 0
  15936 = N 1 0
  15936 = N 2 0
  15936 = N 6 0
  16416 = N 1 48
  16416 = N 2 48
  16416 = N 6 0
  16560 = N 1 96
  16560 = N 3 96
  16560 = N 6 0
  16704 = N 2 0
  16704 = N 3 0
  16704 = N 6 0
  17184 = N 2 24
  17184 = N 3 24
  17184 = N 6 0
  17328 = N 3 144
  17328 = N 6 0
  17472 = N 1 384
  17472 = N 3 384
  17472 = N 6 0
  17952 = N 1 48
  17952 = N 3 48
  17952 = N 6 0
  18096 = N 1 24
  18096 = N 2 24
  18096 = N 6 0
  18240 = N 0 384
  18240 = N 1 384
  18240 = N 6 0
  18720 = N 0 48
  18720 = N 1 48
  18720 = N 6 0
  18864 = N 0 0
  18864 = N 2 0
  18864 = N 6 0
  19008 = N 1 384
  19008 = N 2 384
  19008 = N 6 0
  19488 = N 0 48
  19488 = N 2 48
  19488 = N 6 0
  19632 = N 1 48
  19632 = N 2 48
  19632 = N 6 0
  19776 = N 0 384
  19776 = N 2 384
  19776 = N 6 0
  20256 = N 1 48
  20256 = N 2 48
  20256 = N 6 0
  20400 = N 0 48
  20400 = N 2 48
  20400 = N 6 0
  20544 = N 1 192
  20544 = N 2 192
  20544 = N 6 0
  21024 = N 1 48
  21024 = N 2 48
  21024 = N 6 0
  21168 = N 1 144
  21168 = N 2 144
  21168 = N 6 0
  21312 = N 0 384
  21312 = N 2 384
  21312 = N 6 0
  21792 = N 0 48
  21792 = N 1 48
  21792 = N 6 0
  21936 = N 0 24
  21936 = N 2 24
  21936 = N 6 0
  22080 = N 1 384
  22080 = N 2 384
  22080 = N 6 0
  22560 = N 1 48
  22560 = N 2 48
  22560 = N 6 0
  22704 = N 1 144
  22704 = N 3 144
  22704 = N 6 0
  22848 = N 2 384
  22848 = N 3 384
  22848 = N 6 0
  23328 = N 2 48
  23328 = N 3 48
  23328 = N 6 0
  23472 = N 3 144
  23472 = N 6 0
  23616 = N 1 384
  23616 = N 2 384
  23616 = N 6 0
  24096 = N 1 48
  24096 = N 2 48
  24096 = N 6 0
  24240 = N 0 48
  24240 = N 2 48
  24240 = N 6 0
  24384 = N 0 384
  24384 = N 1 384
  24384 = N 6 0
  24864 = N 0 48
  24864 = N 6 0
  25008 = N 0 24
  25008 = N 1 24
  25008 = N 6 0
  25152 = N 1 0
  25152 = N 2 0
  25152 = N 6 0
  25632 = N 0 48
  25632 = N 2 48
  25632 = N 6 0
  25776 = N 1 96
  25776 = N 2 96
  25776 = N 6 0
  25920 = N 0 0
  25920 = N 2 0
  25920 = N 6 0
  26400 = N 0 48
  26400 = N 2 48
  26400 = N 6 0
  26544 = N 0 144
  26544 = N 1 144
  26544 = N 6 0
  26688 = N 1 384
  26688 = N 2 384
  26688 = N 6 0
  27168 = N 1 48
  27168 = N 2 48
  27168 = N 6 0
  27312 = N 1 24
  27312 = N 2 24
  27312 = N 6 0
  27456 = N 0 384
  27456 = N 1 384
  27456 = N 6 0
  27936 = N 0 48
  27936 = N 6 0
  28080 = N 0 0
  28080 = N 1 0
  28080 = N 6 0
  28224 = N 1 0
  28224 = N 2 0
  28224 = N 6 0
  28704 = N 1 48
  28704 = N 3 48
  28704 = N 6 0
  28848 = N 1 96
  28848 = N 2 96
  28848 = N 6 0
  28992 = N 0 0
  28992 = N 2 0
  28992 = N 6 0
  29472 = N 0 48
  29472 = N 2 48
  29472 = N 6 0
  29616 = N 0 144
  29616 = N 2 144
  29616 = N 6 0
  29760 = N 1 384
  29760 = N 3 384
  29760 = N 6 0
  30240 = N 1 48
  30240 = N 3 48
  30240 = N 6 0
  30384 = N 1 24
  30384 = N 3 24
  30384 = N 6 0
  30528 = N 0 384
  30528 = N 1 384
  30528 = N 6 0
  31008 = N 0 48
  31008 = N 6 0
  31152 = N 0 0
  31152 = N 1 0
  31152 = N 6 0
  31296 = N 1 0
  31296 = N 2 0
  31296 = N 6 0
  31776 = N 1 48
  31776 = N 2 48
  31776 = N 6 0
  31920 = N 1 96
  31920 = N 2 96
  31920 = N 6 0
  32064 = N 0 0
  32064 = N 2 0
  32064 = N 6 0
  32544 = N 0 48
  32544 = N 2 48
  32544 = N 6 0
  32688 = N 0 144
  32688 = N 2 144
  32688 = N 6 0
  32832 = N 1 384
  32832 = N 3 384
  32832 = N 6 0
  33312 = N 1 48
  33312 = N 3 48
  33312 = N 6 0
  33456 = N 1 24
  33456 = N 3 24
  33456 = N 6 0
  33600 = N 0 384
  33600 = N 1 384
  33600 = N 6 0
  34080 = N 0 48
  34080 = N 1 48
  34080 = N 6 0
  34224 = N 0 0
  34224 = N 1 0
  34224 = N 6 0
  34368 = N 3 96
  34368 = N 6 0
  34464 = N 1 96
  34464 = N 6 0
  34560 = N 0 96
  34560 = N 6 0
  34656 = N 3 96
  34656 = N 6 0
  34752 = N 1 96
  34752 = N 6 0
  34848 = N 0 96
  34848 = N 6 0
  34944 = N 3 96
  34944 = N 6 0
  35040 = N 0 96
  35040 = N 6 0
  35136 = N 2 96
  35136 = N 6 0
  35232 = N 1 96
  35232 = N 6 0
  35328 = N 0 96
  35328 = N 6 0
  35424 = N 2 96
  35424 = N 6 0
  35520 = N 1 96
  35520 = N 6 0
  35616 = N 0 96
  35616 = N 6 0
  35712 = N 2 96
  35712 = N 6 0
  35808 = N 0 96
  35808 = N 6 0
  35904 = N 3 96
  35904 = N 6 0
  36000 = N 2 96
  36000 = N 6 0
  36096 = N 1 96
  36096 = N 6 0
  36192 = N 3 96
  36192 = N 6 0
  36288 = N 2 96
  36288 = N 6 0
  36384 = N 1 96
  36384 = N 6 0
  36480 = N 3 96
  36480 = N 6 0
  36576 = N 1 96
  36576 = N 6 0
  36672 = N 2 96
  36672 = N 6 0
  36768 = N 1 96
  36768 = N 6 0
  36864 = N 0 96
  36864 = N 6 0
  36960 = N 2 96
  36960 = N 6 0
  37056 = N 1 96
  37056 = N 6 0
  37152 = N 0 96
  37152 = N 6 0
  37248 = N 2 96
  37248 = N 6 0
  37344 = N 0 96
  37344 = N 6 0
  37440 = N 3 96
  37440 = N 6 0
  37536 = N 1 96
  37536 = N 6 0
  37632 = N 0 96
  37632 = N 6 0
  37728 = N 3 96
  37728 = N 6 0
  37824 = N 1 96
  37824 = N 6 0
  37920 = N 0 96
  37920 = N 6 0
  38016 = N 3 96
  38016 = N 6 0
  38112 = N 0 96
  38112 = N 6 0
  38208 = N 2 96
  38208 = N 6 0
  38304 = N 1 96
  38304 = N 6 0
  38400 = N 0 96
  38400 = N 6 0
  38496 = N 2 96
  38496 = N 6 0
  38592 = N 1 96
  38592 = N 6 0
  38688 = N 0 96
  38688 = N 6 0
  38784 = N 2 96
  38784 = N 6 0
  38880 = N 0 96
  38880 = N 6 0
  38976 = N 3 96
  38976 = N 6 0
  39072 = N 2 96
  39072 = N 6 0
  39168 = N 1 96
  39168 = N 6 0
  39264 = N 3 96
  39264 = N 6 0
  39360 = N 2 96
  39360 = N 6 0
  39456 = N 1 96
  39456 = N 6 0
  39552 = N 3 96
  39552 = N 6 0
  39648 = N 1 96
  39648 = N 6 0
  39744 = N 2 96
  39744 = N 6 0
  39840 = N 1 96
  39840 = N 6 0
  39936 = N 0 96
  39936 = N 6 0
  40032 = N 2 96
  40032 = N 6 0
  40128 = N 1 96
  40128 = N 6 0
  40224 = N 0 96
  40224 = N 6 0
  40320 = N 2 96
  40320 = N 6 0
  40416 = N 0 96
  40416 = N 6 0
  40512 = N 3 96
  40512 = N 6 0
  40608 = N 1 96
  40608 = N 6 0
  40704 = N 0 96
  40704 = N 6 0
  40800 = N 3 96
  40800 = N 6 0
  40896 = N 1 96
  40896 = N 6 0
  40992 = N 0 96
  40992 = N 6 0
  41088 = N 3 96
  41088 = N 6 0
  41184 = N 0 96
  41184 = N 6 0
  41280 = N 2 96
  41280 = N 6 0
  41376 = N 1 96
  41376 = N 6 0
  41472 = N 0 96
  41472 = N 6 0
  41568 = N 2 96
  41568 = N 6 0
  41664 = N 1 96
  41664 = N 6 0
  41760 = N 0 96
  41760 = N 6 0
  41856 = N 2 96
  41856 = N 6 0
  41952 = N 0 96
  41952 = N 6 0
  42048 = N 3 96
  42048 = N 6 0
  42144 = N 2 96
  42144 = N 6 0
  42240 = N 1 96
  42240 = N 6 0
  42336 = N 3 96
  42336 = N 6 0
  42432 = N 2 96
  42432 = N 6 0
  42528 = N 1 96
  42528 = N 6 0
  42624 = N 3 96
  42624 = N 6 0
  42720 = N 1 96
  42720 = N 6 0
  42816 = N 2 96
  42816 = N 6 0
  42912 = N 1 96
  42912 = N 6 0
  43008 = N 0 96
  43008 = N 6 0
  43104 = N 2 96
  43104 = N 6 0
  43200 = N 1 96
  43200 = N 6 0
  43296 = N 0 96
  43296 = N 6 0
  43392 = N 2 96
  43392 = N 6 0
  43488 = N 0 96
  43488 = N 6 0
  43584 = N 3 96
  43584 = N 6 0
  43680 = N 1 96
  43680 = N 6 0
  43776 = N 0 96
  43776 = N 6 0
  43872 = N 3 96
  43872 = N 6 0
  43968 = N 1 96
  43968 = N 6 0
  44064 = N 0 96
  44064 = N 6 0
  44160 = N 3 96
  44160 = N 6 0
  44256 = N 0 96
  44256 = N 6 0
  44352 = N 2 96
  44352 = N 6 0
  44448 = N 1 96
  44448 = N 6 0
  44544 = N 0 96
  44544 = N 6 0
  44640 = N 2 96
  44640 = N 6 0
  44736 = N 1 96
  44736 = N 6 0
  44832 = N 0 96
  44832 = N 6 0
  44928 = N 2 96
  44928 = N 6 0
  45024 = N 0 96
  45024 = N 6 0
  45120 = N 3 96
  45120 = N 6 0
  45216 = N 2 96
  45216 = N 6 0
  45312 = N 1 96
  45312 = N 6 0
  45408 = N 3 96
  45408 = N 6 0
  45504 = N 2 96
  45504 = N 6 0
  45600 = N 1 96
  45600 = N 6 0
  45696 = N 3 96
  45696 = N 6 0
  45792 = N 1 96
  45792 = N 6 0
  45888 = N 2 96
  45888 = N 6 0
  45984 = N 1 96
  45984 = N 6 0
  46080 = N 0 96
  46080 = N 6 0
  46176 = N 2 96
  46176 = N 6 0
  46272 = N 1 96
  46272 = N 6 0
  46368 = N 0 96
  46368 = N 6 0
  46464 = N 2 96
  46464 = N 6 0
  46560 = N 0 96
  46560 = N 6 0
  46656 = N 3 96
  46656 = N 6 0
  46752 = N 1 96
  46752 = N 6 0
  46848 = N 0 96
  46848 = N 6 0
  46944 = N 3 96
  46944 = N 6 0
  47040 = N 1 96
  47040 = N 6 0
  47136 = N 0 96
  47136 = N 6 0
  47232 = N 3 96
  47232 = N 6 0
  47328 = N 0 96
  47328 = N 6 0
  47424 = N 2 96
  47424 = N 6 0
  47520 = N 1 96
  47520 = N 6 0
  47616 = N 0 96
  47616 = N 6 0
  47712 = N 2 96
  47712 = N 6 0
  47808 = N 1 96
  47808 = N 6 0
  47904 = N 0 96
  47904 = N 6 0
  48000 = N 2 96
  48000 = N 6 0
  48096 = N 0 96
  48096 = N 6 0
  48192 = N 3 96
  48192 = N 6 0
  48288 = N 2 96
  48288 = N 6 0
  48384 = N 1 96
  48384 = N 6 0
  48480 = N 3 96
  48480 = N 6 0
  48576 = N 2 96
  48576 = N 6 0
  48672 = N 1 96
  48672 = N 6 0
  48768 = N 3 96
  48768 = N 6 0
  48864 = N 1 96
  48864 = N 6 0
  48960 = N 2 96
  48960 = N 6 0
  49056 = N 1 96
  49056 = N 6 0
  49152 = N 0 96
  49152 = N 6 0
  49248 = N 2 96
  49248 = N 6 0
  49344 = N 1 96
  49344 = N 6 0
  49440 = N 0 96
  49440 = N 6 0
  49536 = N 2 96
  49536 = N 6 0
  49632 = N 0 96
  49632 = N 6 0
  49728 = N 3 96
  49728 = N 6 0
  49824 = N 1 96
  49824 = N 6 0
  49920 = N 0 96
  49920 = N 6 0
  50016 = N 3 96
  50016 = N 6 0
  50112 = N 1 96
  50112 = N 6 0
  50208 = N 0 96
  50208 = N 6 0
  50304 = N 3 96
  50304 = N 6 0
  50400 = N 0 96
  50400 = N 6 0
  50496 = N 2 96
  50496 = N 6 0
  50592 = N 1 96
  50592 = N 6 0
  50688 = N 0 96
  50688 = N 6 0
  50784 = N 2 96
  50784 = N 6 0
  50880 = N 1 96
  50880 = N 6 0
  50976 = N 0 96
  50976 = N 6 0
  51072 = N 2 96
  51072 = N 6 0
  51168 = N 0 96
  51168 = N 6 0
  51264 = N 3 96
  51264 = N 6 0
  51360 = N 2 96
  51360 = N 6 0
  51456 = N 1 96
  51456 = N 6 0
  51552 = N 3 96
  51552 = N 6 0
  51648 = N 2 96
  51648 = N 6 0
  51744 = N 1 96
  51744 = N 6 0
  51840 = N 3 96
  51840 = N 6 0
  51936 = N 1 96
  51936 = N 6 0
  52032 = N 2 96
  52032 = N 6 0
  52128 = N 1 96
  52128 = N 6 0
  52224 = N 0 96
  52224 = N 6 0
  52320 = N 2 96
  52320 = N 6 0
  52416 = N 1 96
  52416 = N 6 0
  52512 = N 0 96
  52512 = N 6 0
  52608 = N 2 96
  52608 = N 6 0
  52704 = N 0 96
  52704 = N 6 0
  52800 = N 3 96
  52800 = N 6 0
  52896 = N 1 96
  52896 = N 6 0
  52992 = N 0 96
  52992 = N 6 0
  53088 = N 3 96
  53088 = N 6 0
  53184 = N 1 96
  53184 = N 6 0
  53280 = N 0 96
  53280 = N 6 0
  53376 = N 3 96
  53376 = N 6 0
  53472 = N 0 96
  53472 = N 6 0
  53568 = N 2 96
  53568 = N 6 0
  53664 = N 1 96
  53664 = N 6 0
  53760 = N 0 96
  53760 = N 6 0
  53856 = N 2 96
  53856 = N 6 0
  53952 = N 1 96
  53952 = N 6 0
  54048 = N 0 96
  54048 = N 6 0
  54144 = N 2 96
  54144 = N 6 0
  54240 = N 0 96
  54240 = N 6 0
  54336 = N 3 96
  54336 = N 6 0
  54432 = N 2 96
  54432 = N 6 0
  54528 = N 1 96
  54528 = N 6 0
  54624 = N 3 96
  54624 = N 6 0
  54720 = N 2 96
  54720 = N 6 0
  54816 = N 1 96
  54816 = N 6 0
  54912 = N 3 96
  54912 = N 6 0
  55008 = N 1 96
  55008 = N 6 0
  55104 = N 2 96
  55104 = N 6 0
  55200 = N 1 96
  55200 = N 6 0
  55296 = N 0 96
  55296 = N 6 0
  55392 = N 2 96
  55392 = N 6 0
  55488 = N 1 96
  55488 = N 6 0
  55584 = N 0 96
  55584 = N 6 0
  55680 = N 2 96
  55680 = N 6 0
  55776 = N 0 96
  55776 = N 6 0
  55872 = N 3 96
  55872 = N 6 0
  55968 = N 1 96
  55968 = N 6 0
  56064 = N 0 96
  56064 = N 6 0
  56160 = N 3 96
  56160 = N 6 0
  56256 = N 1 96
  56256 = N 6 0
  56352 = N 0 96
  56352 = N 6 0
  56448 = N 3 96
  56448 = N 6 0
  56544 = N 2 96
  56544 = N 6 0
  56640 = N 3 96
  56640 = N 6 0
  56736 = N 2 96
  56736 = N 6 0
  56832 = N 1 96
  56832 = N 6 0
  56928 = N 3 96
  56928 = N 6 0
  57024 = N 2 96
  57024 = N 6 0
  57120 = N 1 96
  57120 = N 6 0
  57216 = N 2 96
  57216 = N 6 0
  57312 = N 3 96
  57312 = N 6 0
  57408 = N 2 72
  57408 = N 6 0
  57504 = N 1 96
  57504 = N 6 0
  57600 = N 0 96
  57600 = N 6 0
  57696 = N 2 96
  57696 = N 6 0
  57792 = N 1 96
  57792 = N 6 0
  57888 = N 0 96
  57888 = N 6 0
  57984 = N 2 96
  57984 = N 6 0
  58080 = N 3 96
  58080 = N 6 0
  58176 = N 2 96
  58176 = N 6 0
  58272 = N 1 96
  58272 = N 6 0
  58368 = N 0 96
  58368 = N 6 0
  58464 = N 2 96
  58464 = N 6 0
  58560 = N 1 96
  58560 = N 6 0
  58656 = N 0 96
  58656 = N 6 0
  58944 = N 1 0
  58944 = N 2 0
  58944 = N 6 0
  59424 = N 1 48
  59424 = N 2 48
  59424 = N 6 0
  59568 = N 1 96
  59568 = N 2 96
  59568 = N 6 0
  59712 = N 0 0
  59712 = N 2 0
  59712 = N 6 0
  60192 = N 0 48
  60192 = N 2 48
  60192 = N 6 0
  60336 = N 0 144
  60336 = N 2 144
  60336 = N 6 0
  60480 = N 1 384
  60480 = N 3 384
  60480 = N 6 0
  60960 = N 1 48
  60960 = N 3 48
  60960 = N 6 0
  61104 = N 1 24
  61104 = N 3 24
  61104 = N 6 0
  61248 = N 0 384
  61248 = N 1 384
  61248 = N 6 0
  61728 = N 0 48
  61728 = N 1 48
  61728 = N 6 0
  61872 = N 0 0
  61872 = N 1 0
  61872 = N 6 0
  62016 = N 1 0
  62016 = N 2 0
  62016 = N 6 0
  62496 = N 1 48
  62496 = N 2 48
  62496 = N 6 0
  62640 = N 1 96
  62640 = N 3 96
  62640 = N 6 0
  62784 = N 2 0
  62784 = N 3 0
  62784 = N 6 0
  63264 = N 2 24
  63264 = N 3 24
  63264 = N 6 0
  63408 = N 3 144
  63408 = N 6 0
  63552 = N 1 384
  63552 = N 3 384
  63552 = N 6 0
  64032 = N 1 48
  64032 = N 3 48
  64032 = N 6 0
  64176 = N 1 24
  64176 = N 2 24
  64176 = N 6 0
  64320 = N 0 384
  64320 = N 1 384
  64320 = N 6 0
  64800 = N 0 48
  64800 = N 1 48
  64800 = N 6 0
  64944 = N 0 0
  64944 = N 2 0
  64944 = N 6 0
  65088 = N 1 0
  65088 = N 2 0
  65088 = N 6 0
  65568 = N 1 48
  65568 = N 2 48
  65568 = N 6 0
  65712 = N 1 96
  65712 = N 2 96
  65712 = N 6 0
  65856 = N 0 0
  65856 = N 2 0
  65856 = N 6 0
  66336 = N 0 48
  66336 = N 2 48
  66336 = N 6 0
  66480 = N 0 144
  66480 = N 2 144
  66480 = N 6 0
  66624 = N 1 384
  66624 = N 3 384
  66624 = N 6 0
  67104 = N 1 48
  67104 = N 3 48
  67104 = N 6 0
  67248 = N 1 24
  67248 = N 3 24
  67248 = N 6 0
  67392 = N 0 384
  67392 = N 1 384
  67392 = N 6 0
  67872 = N 0 48
  67872 = N 1 48
  67872 = N 6 0
  68016 = N 0 0
  68016 = N 1 0
  68016 = N 6 0
  68160 = N 1 0
  68160 = N 2 0
  68160 = N 6 0
  68640 = N 1 48
  68640 = N 2 48
  68640 = N 6 0
  68784 = N 1 96
  68784 = N 3 96
  68784 = N 6 0
  68928 = N 2 0
  68928 = N 3 0
  68928 = N 6 0
  69408 = N 2 24
  69408 = N 3 24
  69408 = N 6 0
  69552 = N 3 144
  69552 = N 6 0
  69696 = N 1 384
  69696 = N 3 384
  69696 = N 6 0
  70176 = N 1 48
  70176 = N 3 48
  70176 = N 6 0
  70320 = N 1 24
  70320 = N 2 24
  70320 = N 6 0
  70464 = N 0 384
  70464 = N 1 384
  70464 = N 6 0
  70944 = N 0 48
  70944 = N 1 48
  70944 = N 6 0
  71088 = N 0 0
  71088 = N 2 0
  71088 = N 6 0
  71232 = N 1 0
  71232 = N 2 0
  71232 = N 6 0
  71712 = N 1 48
  71712 = N 2 48
  71712 = N 6 0
  71856 = N 1 96
  71856 = N 2 96
  71856 = N 6 0
  72000 = N 0 0
  72000 = N 2 0
  72000 = N 6 0
  72480 = N 0 48
  72480 = N 2 48
  72480 = N 6 0
  72624 = N 0 144
  72624 = N 2 144
  72624 = N 6 0
  72768 = N 1 384
  72768 = N 3 384
  72768 = N 6 0
  73248 = N 1 48
  73248 = N 3 48
  73248 = N 6 0
  73392 = N 1 24
  73392 = N 3 24
  73392 = N 6 0
  73536 = N 0 384
  73536 = N 1 384
  73536 = N 6 0
  74016 = N 0 48
  74016 = N 1 48
  74016 = N 6 0
  74160 = N 0 0
  74160 = N 1 0
  74160 = N 6 0
  74304 = N 1 0
  74304 = N 2 0
  74304 = N 6 0
  74784 = N 1 48
  74784 = N 2 48
  74784 = N 6 0
  74928 = N 1 96
  74928 = N 3 96
  74928 = N 6 0
  75072 = N 2 0
  75072 = N 3 0
  75072 = N 6 0
  75552 = N 2 24
  75552 = N 3 24
  75552 = N 6 0
  75696 = N 3 144
  75696 = N 6 0
  75840 = N 1 384
  75840 = N 3 384
  75840 = N 6 0";
    };
}
