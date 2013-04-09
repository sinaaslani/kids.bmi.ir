using System;

namespace Kids.Utility.Ping_Helper
{
    internal class IcmpPacket
    {
        private const int ICMP_ECHO = 8;
        private const int PING_DATA_SIZE = 32; //sizeof(IcmpPacket) - 8;
        public const int ICMP_PACKET_SIZE = PING_DATA_SIZE + 8;
        private readonly Byte[] Data;

        private readonly UInt16 Identifier; // identifier
        private readonly UInt16 SequenceNumber; // sequence number  
        private readonly Byte SubCode; // type of sub code
        private readonly Byte Type; // type of message
        private UInt16 CheckSum; // ones complement CalculateChecksum of struct

        public IcmpPacket()
        {
            // Construct the this to send
            Type = ICMP_ECHO; //8
            SubCode = 0;
            CheckSum = UInt16.Parse("0");
            Identifier = UInt16.Parse("45");
            SequenceNumber = UInt16.Parse("0");

            Data = new Byte[PING_DATA_SIZE];

            //Initilize the Packet.Data
            for (int i = 0; i < Data.Length; i++)
                Data[i] = (byte) '#';

            //Create initial checksum
            UpdateChecksum();
        }

        /// <summary>
        /// Converts an IcmpPacket to a byte array.
        /// </summary>
        /// <returns>Byte array of contents of ICMP packet</returns>
        public byte[] ToByteArray()
        {
            //Variable to hold the total Packet size
            var buffer = new byte[ICMP_PACKET_SIZE];
            int index = 0;

            var b_type = new byte[1];
            b_type[0] = (Type);

            var b_code = new byte[1];
            b_code[0] = (SubCode);

            byte[] b_cksum = BitConverter.GetBytes(CheckSum);
            byte[] b_id = BitConverter.GetBytes(Identifier);
            byte[] b_seq = BitConverter.GetBytes(SequenceNumber);

            Array.Copy(b_type, 0, buffer, index, b_type.Length);
            index += b_type.Length;

            Array.Copy(b_code, 0, buffer, index, b_code.Length);
            index += b_code.Length;

            Array.Copy(b_cksum, 0, buffer, index, b_cksum.Length);
            index += b_cksum.Length;

            Array.Copy(b_id, 0, buffer, index, b_id.Length);
            index += b_id.Length;

            Array.Copy(b_seq, 0, buffer, index, b_seq.Length);
            index += b_seq.Length;

            // copy the data	        
            Array.Copy(Data, 0, buffer, index, PING_DATA_SIZE);
            index += PING_DATA_SIZE;

            if (index != ICMP_PACKET_SIZE) //sizeof(IcmpPacket)
                return null;
            else
                return buffer;
        }

        /// <summary>
        /// Converts ICMP packet to UInt16 array
        /// </summary>
        /// <returns>UInt16 array of contents of ICMP packet</returns>
        public UInt16[] ToUInt16Array()
        {
            //Get the Half size of the Packet
            var checksumBufferLength = (int) Math.Ceiling((double) ICMP_PACKET_SIZE/2);

            //Create byte array
            byte[] buffer = ToByteArray();
            if (buffer == null)
                return null;

            //Create a UInt16 Array
            var checksumBuffer = new UInt16[checksumBufferLength];

            //Code to initialize the Uint16 array 
            int icmpHeaderBufferIndex = 0;
            for (int i = 0; i < checksumBufferLength; i++)
            {
                checksumBuffer[i] = BitConverter.ToUInt16(buffer, icmpHeaderBufferIndex);
                icmpHeaderBufferIndex += 2;
            }

            return checksumBuffer;
        }

        /// <summary>
        /// This Method has the algorithm to make the checksum
        /// </summary>
        public void UpdateChecksum()
        {
            UInt16[] buffer;
            int cksum = 0;
            int counter = 0;
            int size = 0;

            buffer = ToUInt16Array();
            if (buffer == null)
                throw new Exception("Unable to create UInt16 array.  Please check packet properties.");

            size = buffer.Length;

            while (size > 0)
            {
                UInt16 val = buffer[counter];

                cksum += Convert.ToInt32(buffer[counter]);
                counter += 1;
                size -= 1;
            }

            cksum = (cksum >> 16) + (cksum & 0xffff);
            cksum += (cksum >> 16);

            CheckSum = (UInt16) (~cksum);
        }
    }
}