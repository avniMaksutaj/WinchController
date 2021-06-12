using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class DMXController
{
    #region ===== INTERNAL CLASSES
    /// <summary>
    /// Allows DMX channel data to be accessed as an array with bounds of 1 to 512 inclusive.
    /// </summary>
    /// <remarks>
    /// Based on Indexed Properties Tutorial:
    ///     https://msdn.microsoft.com/en-us/library/aa288464(v=vs.71).aspx	
    /// </remarks>
    public class DmxChannelCollection
    {
        /// <summary>
        /// The DMX instance that parents this collection.
        /// </summary>
        readonly DMXController dmx;

        /// <summary>
        /// Initializes a new instance of the <see cref="DMXController+DmxChannelCollection"/> class.
        /// </summary>
        /// <param name="dmx">Dmx.</param>
        internal DmxChannelCollection(DMXController dmx)
        {
            this.dmx = dmx;
        }

        /// <summary>
        /// Gets or sets the <see cref="DMXController+DmxChannelCollection"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public byte this[int index]
        {
            get
            {
                if (index < 1 || index > 512)
                    throw new IndexOutOfRangeException();
                else
                    return this.dmx.buffer[index + DMXController.INDEX_OFFSET];
            }
            set
            {
                if (index < 1 || index > 512)
                    throw new IndexOutOfRangeException();
                else
                    this.dmx.buffer[index + DMXController.INDEX_OFFSET] = value;
            }
        }
    }
    #endregion

    #region ===== PRIVATE DECLARATIONS
    /// <summary>
    /// The length of the DMX buffer.
    /// </summary>
    /// Refer to <see cref="buffer"/> for details of buffer layout.
    public const int BUFFER_LENGTH = 518;

    /// <summary>
    /// DMX message buffer.  
    /// 
    /// Bytes  Index  Description
    /// ------+------+--------------------------------------------------
    ///    1     0    0x7E - Start of USB Pro Message
    ///    1     1    0x06 - Instructs USB Pro to send DMX Data
    ///    1     2    0x01 - LSB of DMX data length (513 = 512 + SC)
    ///    1     3    0x10 - MSB of DMX data length (513 = 512 + SC)
    ///    1     4    0x00 - DMX Start Code (SC) 
    ///   512  5-516  DMX Channel Data
    ///    1    517   0xE7 - End of USB Pro Message
    /// ------+----------------------------------------------------------
    ///  518  = 512 channels + 6 bytes message overhead.
    /// </summary>
    public byte[] buffer = new byte[BUFFER_LENGTH];

    /// <summary>
    /// The offset between a DMX channel number and it's corresponding
    /// index in the buffer.
    /// </summary>
    private const int INDEX_OFFSET = 4;

    /// <summary>
    /// The instance of the serial port used to communicate with the DMX controller.
    /// </summary>
    private SerialPort dmxPort;
    #endregion

    /// <summary>
    /// Gets or sets the data for a given DMX channel in the range of 1 to 512 inclusive.
    /// </summary>
    public readonly DmxChannelCollection Channels;

    /// <summary>
    /// Initializes a new instance of the Enttec <see cref="DMX"/> USB Pro interface.
    /// </summary>
    public DMXController(string COMport)
    {
        try
        {
            this.Channels = new DmxChannelCollection(this);

            // Initialize the DMX Message Array with zeros
            for (int i = 0; i < BUFFER_LENGTH; i++) this.buffer[i] = (byte)0x00;

            // Initialize pre and post amble.
            this.buffer[000] = (byte)0x7E;   // ENTTEC Start Of Message delimiter
            this.buffer[001] = (byte)0x06;   // ENTTEC Message Label
            this.buffer[002] = (byte)0x01;   // Data Length / LSB of 513
            this.buffer[003] = (byte)0x02;   // Data Length / MSB of 513
            this.buffer[004] = (byte)0x00;   // DMX Start Code
            this.buffer[517] = (byte)0xE7;   // ENTTEC End Of Message delimiter

            // HOWTO: Specify Serial Ports Larger than COM9: 
            //     http://support.microsoft.com/kb/115831
            this.dmxPort = new SerialPort(@"\\.\" + COMport);

            this.dmxPort.Open();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Close()
    {
        dmxPort.Close();
    }


    /// <summary>
    /// Release unmanaged resources.
    /// </summary>
    ~DMXController()
    {
        dmxPort.Close();
        dmxPort.Dispose();
    }

    /// <summary>
    /// Send the DMX data.
    /// </summary>
    public void Send()
    {
        this.dmxPort.Write(buffer, 0, BUFFER_LENGTH);
    }
}