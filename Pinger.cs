using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;

namespace DynamicsStressTest
{
    class Pinger
    {
        public int totalPingsSent = 0;

        public int[] SendPing(String address, int timeOut)
        {

            int[] completeTime = new int[2]; 
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(address, timeOut);
                if (reply != null)
                {
                    completeTime[0] = (int)reply.RoundtripTime;
                    completeTime[1] = 1;
                }
            }
            catch
            {
                completeTime[0] = timeOut;
                completeTime[1] = 0;
            }
            finally
            {
                totalPingsSent++;
            }
            return completeTime;
        }
    }
 }
