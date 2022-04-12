/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author user
 */
public class GestioneUdp {
    private DatagramSocket  server;
        private static GestioneUdp instance = null;

        private GestioneUdp()
        {
        try {
            server = new DatagramSocket(12345);
            } catch (SocketException ex) {
                System.out.println("Impossibile aprire il server");
            }
        }
        public static GestioneUdp Instance()
        {
            if (instance == null)
                instance = new GestioneUdp();
            return instance;
        }
        public  void Invia(Messaggio m) throws UnknownHostException, IOException
        {
            byte[] buf= m.Testo.getBytes();
            DatagramPacket p= new DatagramPacket(buf,buf.length,InetAddress.getByName(m.Ip), m.Porta);
            server.send(p);
        }
        public  Messaggio Ricevi() throws IOException
        {
            byte[] buf= new byte[1000000];
            DatagramPacket p= new DatagramPacket(buf,buf.length);
            server.receive(p);
            Messaggio m=new Messaggio();
            m.Ip=p.getAddress().getHostAddress();
            m.Porta=p.getPort();
            m.Testo=new String(p.getData(),0,p.getLength());
            return m;
        }
}
