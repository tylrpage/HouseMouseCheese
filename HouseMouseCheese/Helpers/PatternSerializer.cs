using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using Google.Protobuf;
using Proto = Google.Protobuf.Cheese;

namespace HouseMouseCheese.Helpers
{
    public static class PatternSerializer
    {
        private static Int32 ColorToInt32(Color color)
        {
            byte[] bytes = new byte[4];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            bytes[3] = color.A;
            return BitConverter.ToInt32(bytes, 0);
        }

        private static Color Int32ToColor(Int32 colorInt)
        {
            byte[] bytes = BitConverter.GetBytes(colorInt);
            Color color = new Color();
            color.R = bytes[0];
            color.G = bytes[1];
            color.B = bytes[2];
            color.A = bytes[3];
            return color;
        }

        private static Proto.Pattern GenerateProtoPattern(Pattern pattern)
        {
            Proto.Pattern protoPattern = new Proto.Pattern();
            protoPattern.Width = ConfigConstant.GetInt("FRAME_WIDTH");
            protoPattern.Height = ConfigConstant.GetInt("FRAME_HEIGHT");
            protoPattern.FramesPerBeat = pattern.FramesPerBeat;
            protoPattern.UniqueCycles = pattern.UniqueCycles;
            foreach (Frame frame in pattern.Frames)
            {
                Proto.Frame protoFrame = new Proto.Frame();
                foreach (Pixel pixel in frame.Pixels)
                {
                    protoFrame.Pixels.Add(ColorToInt32(pixel.Color));
                }
                protoPattern.Frames.Add(protoFrame);
            }

            return protoPattern;
        }

        private static Pattern GeneratePattern(Proto.Pattern protoPattern)
        {
            Pattern pattern = new Pattern();
            pattern.FramesPerBeat = protoPattern.FramesPerBeat;
            pattern.UniqueCycles = protoPattern.UniqueCycles;
            for (int i = 0; i < protoPattern.Frames.Count; i++)
            {
                Frame frame = new Frame(i);
                for (int k = 0; k < protoPattern.Frames[i].Pixels.Count; k++)
                {
                    frame.Pixels[k].Color = Int32ToColor(protoPattern.Frames[i].Pixels[k]);
                }
                pattern.Frames[i] = frame;
            }

            return pattern;
        }

        public static void WritePattern(Pattern pattern, string filename)
        {
            Proto.Pattern protoPattern = GenerateProtoPattern(pattern);
            using (var output = File.OpenWrite(filename))
            {
                protoPattern.WriteTo(output);
            }
        }

        public static Pattern ReadPattern(string filename)
        {
            Proto.Pattern protoPattern;
            using (var input = File.OpenRead(filename))
            {
                protoPattern = Proto.Pattern.Parser.ParseFrom(input);
            }

            return GeneratePattern(protoPattern);
        }
    }
}
