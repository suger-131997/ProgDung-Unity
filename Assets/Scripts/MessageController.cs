﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    public GameObject messageBox;
    public Text message_text;
    [TextArea]
    public string mesasge;
    public int line_num;
    public int line_char_num;

    private string[] message_lines;

    private bool massage_flag = false;
    private int current_line = 0;
    private int update_interval = 0;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<PlayerController>();

        var message_lines_list = new List<string>();
        string line = "";
        foreach (char c in mesasge)
        {
            line += c;
            if(c == '\n')
            {
                message_lines_list.Add(line);
                line = "";
            }
    
            if(line.Length >= line_char_num)
            {
                line += "\n";
                message_lines_list.Add(line);
                line = "";
            }
        }
        Debug.Log(line);
        message_lines_list.Add(line);

        message_lines = message_lines_list.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (massage_flag)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                update_interval++;
            }
            else
            {
                update_interval = 0;
            }
            if (update_interval > 5) { 
                current_line++;
                update_interval = 0;
            }

            if (current_line >= message_lines.Length)
            {
                massage_flag = false;
                messageBox.SetActive(false);
                player.PoseFlag = false;

                return;
            }

            int head = 0;
            if (current_line >= line_num)
            {
                head = current_line - line_num + 1;
            }

            message_text.text = "";

            Debug.Log(current_line);
            for(int i = head; i <= current_line; i++)
            {
                message_text.text += message_lines[i];
            }            
        }

        
    }

    public void openMassage()
    {
        massage_flag = true;
        current_line = 0;
        messageBox.SetActive(true);
        player.PoseFlag = true;
    }
}