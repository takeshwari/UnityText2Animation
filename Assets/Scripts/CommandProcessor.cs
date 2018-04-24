using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class CommandProcessor
{
		public CommandProcessor ()
		{
		}

		public String Parse(String pCmdStr){
			String strResult = "Do not understand";;
			pCmdStr = pCmdStr.ToLower();
			String[] parts = pCmdStr.Split(' '); // tokenise the command
		    
			// process the tokens
			switch( parts[0]){
				case "pick" : 
					if( parts[1] == "up") {
						Debug.Log("Got Pick up");
						strResult = "Got Pick up";

						if( parts.Length == 3){
									String param = parts[2];
						}// do pick up command
						// GameModel.Pickup();
					}
					break;
				case "go" : 
						switch( parts[1]) {
						case"north": 
							Debug.Log("Got go North");
							strResult = "Got Go North";
							break;
						case "south": 
							Debug.Log("Got go South");
							strResult = "Got Go South";
							break;
						default: 
							Debug.Log(" do not know how to go there"); 
							strResult = "Do not know how to go there";
							break;
						}// end switch
					break;
				default:
			        Debug.Log("Do not understand");
					strResult = "Do not understand";
					break;
			         
			}// end switch
		    
			return strResult;

		}// Parse
}


