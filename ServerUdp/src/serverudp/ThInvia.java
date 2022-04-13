/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import static serverudp.DatiCondivisi.sInvia;

/**
 *
 * @author user
 */
public class ThInvia extends Thread{
    public void run(){
        while(!DatiCondivisi.Instance().termina)
        {
            try
            {
                
                sInvia.acquire();
                Messaggio daInviare = DatiCondivisi.Instance().GetMessaggioDaInviare();
                
                if(daInviare!=null)
                { GestioneUdp.Instance().Invia(daInviare);
                }
            } catch (IOException ex) {
            Logger.getLogger(ThRicevi.class.getName()).log(Level.SEVERE, null, ex);
            } catch (InterruptedException ex) {
                Logger.getLogger(ThInvia.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}
