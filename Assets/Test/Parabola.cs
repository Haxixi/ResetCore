﻿using UnityEngine;
{
    //重力
    [Range(0, 1)]
    //最大长度
    public float maxLength = 50;
    //两点之间的距离
    [Range(0, 1)]
    public float length = 0.2f;
    //点集合
    List<Vector3> m_List = new List<Vector3>();
    public void OnRenderObject()
    {
        Vector3 position = transform.position;
        {

        GL.Begin(GL.LINES);
        {
        m_List.Clear();
    void CreateLineMaterial()
    {
        {