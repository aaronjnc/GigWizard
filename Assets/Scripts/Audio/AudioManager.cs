using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public enum SpellTypes
    {
        Light,
        Water,
        Pink
    }

    private Dictionary<SpellTypes, Sound> spellClipsToAudioClipDictionary;

    [SerializeField] private Sound _lightSpell;
    [SerializeField] private Sound _waterSpell;
    [SerializeField] private Sound _pinkSpell;
    [SerializeField] private Sound[] _playerBattleSounds;
    [SerializeField] private Sound[] _enemyBattleSounds;
    [SerializeField] private AudioSource _audioSource;
    private Health[] _enemyHealthComponents;
    private Health _playerHealthComponent;

    public void PlaySpellSound(SpellTypes spellType)
    {
        switch (spellType)
        {
            case SpellTypes.Light:
                _audioSource.PlayOneShot(_lightSpell.clip);
                break;
            case SpellTypes.Water:
                _audioSource.PlayOneShot(_waterSpell.clip);
                break;
            case SpellTypes.Pink:
                _audioSource.PlayOneShot(_pinkSpell.clip);
                break;
        }
    }

    public void PlayEnemyBattleSound()
    {
        _audioSource.PlayOneShot(_enemyBattleSounds[Random.Range(0, _enemyBattleSounds.Length)].clip);
    }

    public void PlayPlayerBattleSound()
    {
        _audioSource.PlayOneShot(_playerBattleSounds[Random.Range(0, _playerBattleSounds.Length)].clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        spellClipsToAudioClipDictionary = new Dictionary<SpellTypes, Sound>
        {
            { SpellTypes.Light, _lightSpell },
            { SpellTypes.Water, _waterSpell },
            { SpellTypes.Pink, _pinkSpell }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
