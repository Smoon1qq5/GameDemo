
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 初始化所有UI 并缓存
/// </summary>
public class UIManager 
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;    
        }
    }   
    
    //ui窗口
    private Dictionary<PanelType, BaseUIPanel> ui_dic = new Dictionary<PanelType, BaseUIPanel>();

   
    //resources 文件夹下的ui
    private Dictionary<PanelType, string> ui_path_res_load = new Dictionary<PanelType, string>();

    
    private AlertPanel alert;
    private Stack<BaseUIPanel> ui_stack = new Stack<BaseUIPanel>();

    public UIManager()
    {
        Init();
    }
    ~UIManager()
    {
        instance=null;
        ui_stack.Clear();
        ui_dic.Clear();
        ui_path_res_load.Clear();
    }


    public  void Init()
    {      
        InitAddUI();
        InitAddUILogin();
    
        
    }

    /// <summary>
    ///  把Resources文件下的UI路径放在字典里
    /// </summary>
    private void InitAddUI()
    {
        string panelPath = "Version1.0/UIPrefab/Room/";
        string[] path = new string[] { "RoomListPanel", "RoomPanel", "GamePanel", "MainPanel" };
        ui_path_res_load.Add(PanelType.RoomList, panelPath + path[0]);
        ui_path_res_load.Add(PanelType.Room, panelPath + path[1]);
        ui_path_res_load.Add(PanelType.Game, panelPath + path[2]);
        ui_path_res_load.Add(PanelType.MainPanel, panelPath + path[3]);

    }

    private void InitAddUILogin()
    {
        string panelPath = "Version1.0/UIPrefab/Panel/";

        string[] path = new string[] { "AlertPanel", "LoginPanel", "RegisterPanel", "SelectPlayerPanel", "UserInfoPanel" };
        ui_path_res_load.Add(PanelType.Alert, panelPath + path[0]);
        ui_path_res_load.Add(PanelType.Login, panelPath + path[1]);
        ui_path_res_load.Add(PanelType.Register, panelPath + path[2]);
        ui_path_res_load.Add(PanelType.SelectPlayer, panelPath + path[3]);
        ui_path_res_load.Add(PanelType.UserInfo, panelPath + path[4]);
    }

  
    //实例化UI
    private BaseUIPanel SpawnPanel(PanelType panelType, bool is_resload = false)
    {
        if (is_resload == false) return null;
        if (ui_path_res_load.TryGetValue(panelType, out string path))
        {
            GameObject obj =GameObject.Instantiate(Resources.Load(path),GameObject.Find("Canvas").transform) as GameObject;
            BaseUIPanel ui_panel = obj.GetComponent<BaseUIPanel>();
            ui_panel.UI_Manager = this;
            if(!ui_dic.ContainsKey(panelType))
            ui_dic.Add(panelType, ui_panel);
            return ui_panel;
        }
        return null;
    }
    //把UI显示到界面上
    public BaseUIPanel PushPanel(PanelType panelType, bool is_resload = false)
    {
        if (ui_dic.TryGetValue(panelType, out BaseUIPanel uIPanel))
        {
            if (ui_stack.Count > 0)
            {
                //暂停栈顶的ui
                BaseUIPanel top_ui = ui_stack.Peek();
                top_ui.OnPause();
            }
            ui_stack.Push(uIPanel);
            uIPanel.OnEnter();
            return uIPanel;
        }
        else
        {
            BaseUIPanel uIPanel1;
       
            uIPanel1 = SpawnPanel(panelType, true);
      


            if (ui_stack.Count > 0)
            {
                //暂停栈顶的ui
                BaseUIPanel top_ui = ui_stack.Peek();
                top_ui.OnPause();

            }
            ui_stack.Push(uIPanel1);

            uIPanel1.OnEnter();
            return uIPanel1;
        }
    }

    //关闭当前ui 显示刚才ui
    public void PopPanel()
    {

        Pop();

        if (ui_stack.Count == 0) return;

        BaseUIPanel panel = ui_stack.Peek();
        panel.OnRecovery();

    }

    public void Pop()
    {
        if (ui_stack.Count == 0) return;

        BaseUIPanel topPanel = ui_stack.Pop();
        topPanel.OnExit();
    }

    //清除栈里的ui
    public void ClearPanelStack()
    {
        ui_stack.Clear();
    }
    //切换场景canvas得更换
    //public void UpdateCanvasOnLoadScene()
    //{
    //    canvas = GameObject.Find("Canvas").transform;
    //}

   public void SetAlertPanel(AlertPanel alert)
    {
        this.alert = alert;
    }


    public void ShouMessage(string str)
    {

        AlertHandle();

        alert.ShowMessage(str);
    }
    public void AlertHandle()
    {
        if (ui_dic.TryGetValue(PanelType.Alert, out BaseUIPanel uIPanel))
        {
            if (uIPanel != null)
            {
                uIPanel.OnEnter();
            }
            else
            {
                BaseUIPanel uIPanel1 = SpawnPanel(PanelType.Alert, true);
                uIPanel1.OnEnter();
            }

        }
        else
        {
            BaseUIPanel uIPanel1 = SpawnPanel(PanelType.Alert, true);
            uIPanel1.OnEnter();
        }

    }

    #region 利用ab包加载   反射创建脚本对象并添加
    // //ui预制件
    //private Dictionary<PanelType, UnityEngine.Object> ui_prefab= new Dictionary<PanelType, UnityEngine.Object>();
    // //名称 对象
    // private Dictionary<string, BaseUIPanel> panel_obj= new Dictionary<string, BaseUIPanel>();
    // private Dictionary<string, BaseRequest> request_list= new Dictionary<string, BaseRequest>();
    //ab包中的ui 名称
    // private string[] ui_name = new string[] { "AlertPanel", "LoginPanel", "RegisterPanel", "SelectPlayerPanel", "UserInfoPanel" };

    ////从ab包加载预制件  存储到ui_prefab字典里
    //private void LoadUIObject()
    //{

    //    for (int i = 0; i < ui_name.Length; i++)
    //    {
    //        UnityEngine.Object obj = ABManager.Instance.LoadNeedAsset("uiprefab", ui_name[i]);
    //        var name_key = (PanelType)System.Enum.Parse(typeof(PanelType), ui_name[i].Substring(0, ui_name[i].Length - 5));
    //        ui_prefab[name_key] = obj;
    //    }

    //}

    //实例化UI
    //private BaseUIPanel SpawnPanel(PanelType panelType)
    //{
    //    if (ui_prefab.TryGetValue(panelType, out UnityEngine.Object obj))
    //    {


    //        GameObject ui = GameObject.Instantiate(obj) as GameObject;
    //        ui.name = obj.name;
    //        ui.transform.SetParent(canvas);
    //        ui.transform.localPosition = Vector3.zero;
    //        ui.transform.localScale = Vector3.one;
    //        ui.transform.localRotation = Quaternion.identity;

    //        ui.transform.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    //        ui.transform.GetComponent<RectTransform>().offsetMin = Vector2.zero;

    //        ui.AddComponent(panel_obj[ui.name].GetType());
    //        ui.AddComponent(request_list[ui.name.Substring(0, ui.name.Length - 5) + "Request"].GetType());
    //        BaseUIPanel ui_panel = ui.GetComponent<BaseUIPanel>();

    //        ui_dic.Add(panelType, ui_panel);
    //        ui_panel.UI_Manager = this;
    //        return ui_dic[panelType];
    //    }
    //    return null;
    //}

    //private void CreatePanelObjects()
    //{
    //    if (ui_name.Length == 0) return;
    //    for (int i = 0; i < ui_name.Length; i++)
    //    {
    //        Type type = Type.GetType(ui_name[i]);
    //        BaseUIPanel panel = Activator.CreateInstance(type) as BaseUIPanel;
    //        panel_obj.Add(ui_name[i], panel);
    //    }

    //}
    //private void CreateRequestObjects()
    //{
    //    if (ui_name.Length == 0) return;
    //    for (int i = 0; i < ui_name.Length; i++)
    //    {
    //        var request = ui_name[i].Substring(0, ui_name[i].Length - 5) + "Request";

    //        Type type = Type.GetType(request);

    //        BaseRequest re = Activator.CreateInstance(type) as BaseRequest;
    //        request_list.Add(request, re);
    //    }

    //}
    #endregion
}
