/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author user
 */
public class ThRicevi extends Thread{
    public void run(){
        while(!DatiCondivisi.Instance().termina)
        {
            try
            {
                Messaggio m=GestioneUdp.Instance().Ricevi();
                System.out.println("ricevuto"+m.Testo);
                DatiCondivisi.Instance().AddMessaggioRicevuti(m);
            } catch (IOException ex) {
            }
        }
    }
}
