using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicTypes
{
    MenuMusic,
    WanderingMusic,
    BadStuff
}

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<SpellTypes, Sound> _spellClipsToAudioClipDictionary;
    private Dictionary<MusicTypes, Sound> _musicClipsToAudioClipDictionary;

    [SerializeField] private Sound _lightSpell;
    [SerializeField] private Sound _waterSpell;
    [SerializeField] private Sound _plantSpell;
    [SerializeField] private Sound _protectSpell;
    [SerializeField] private Sound _menuMusic;
    [SerializeField] private Sound _wanderingMusic;
    [SerializeField] private Sound _badStuffMusic;
    [SerializeField] private Sound[] _playerBattleSounds;
    [SerializeField] private Sound[] _enemyBattleSounds;
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _bgmAudioSource;

    public void PlayMusic(MusicTypes musicType)
    {
        _bgmAudioSource.clip = _musicClipsToAudioClipDictionary[musicType].clip;
        _bgmAudioSource.Play();
    }

    public void PlaySpellSound(SpellTypes spellType)
    {
        _sfxAudioSource.PlayOneShot(_spellClipsToAudioClipDictionary[spellType].clip);
    }

    public void PlayEnemyBattleSound()
    {
        _sfxAudioSource.PlayOneShot(_enemyBattleSounds[Random.Range(0, _enemyBattleSounds.Length)].clip);
    }

    public void PlayPlayerBattleSound()
    {
        _sfxAudioSource.PlayOneShot(_playerBattleSounds[Random.Range(0, _playerBattleSounds.Length)].clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        _spellClipsToAudioClipDictionary = new Dictionary<SpellTypes, Sound>
        {
            { SpellTypes.LightSpell, _lightSpell },
            { SpellTypes.WaterSpell, _waterSpell },
            { SpellTypes.PlantSpell, _plantSpell },
            { SpellTypes.ProtectSpell, _plantSpell }
        };

        _musicClipsToAudioClipDictionary = new Dictionary<MusicTypes, Sound>
        {
            { MusicTypes.MenuMusic, _menuMusic },
            { MusicTypes.WanderingMusic, _wanderingMusic },
            { MusicTypes.BadStuff, _badStuffMusic }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
