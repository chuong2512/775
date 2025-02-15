﻿using UnityEngine;

public class MusicButton : MonoBehaviour
{
    private SpriteSwapper m_spriteSwapper;
    private bool m_on;

    private void Start()
    {
        m_spriteSwapper = GetComponent<SpriteSwapper>();
        m_on = PlayerPrefs.GetInt("music_on") == 1;
        if (!m_on)
            m_spriteSwapper.SwapSprite();
    }

    public void Toggle()
    {
        m_on = !m_on;
        var backgroundAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        backgroundAudioSource.volume =  m_on ? 1 : 0;
        PlayerPrefs.SetInt("music_on", m_on ? 1 : 0);
    }

    public void ToggleSprite()
    {
        m_on = !m_on;
        m_spriteSwapper.SwapSprite();
    }
}
