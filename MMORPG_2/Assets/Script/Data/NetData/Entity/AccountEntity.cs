using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountEntity  {

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int YuanBao { get; set; }
    public int LastServerId { get; set; }
    public string LastServerName { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}
