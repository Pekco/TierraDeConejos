using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BandMemberData {

    private Character member;
    private int memberPos;
    private bool isEmpty;
    private float speed;

    public void SetMember( Character member) {

        this.member = member;
        //memberAI = member.GetComponent<CharacterControllerAI>();
        isEmpty = false;
    
    }

    public void SetOffset( ) {
    
        
    
    }

    public void SetPos( int pos ) {

        memberPos = pos;
    
    }

    public Character GetMember( ) {

        return member;
    
    }

    public int GetMemberPos( ) {

        return memberPos;
    
    }

    public bool IsEmpty( ) {

        return isEmpty;
    
    }

    public void ClearMember( ) {

        member = null;
        isEmpty = true;
        speed = 0.0f;
    
    }

    public void SetSpeed( float speed ) {

        this.speed = speed;
    
    }

    public float GetSpeed( ) {

        return speed;
    
    }

}

public class BandData {

    private string bandName;
    private int id;

    private Character bandLeader;
    private List<BandMemberData> bandMembers;

    private static int maxNumMembers = 10;
    private float bandSpeed;
    private bandModes bandMode;

    private bool isEmpty;
    private int memberCount;
    private bool isBandSpeed;

    public event Action ChangeBandModeAction;
    public event Action<bool> StandStillAllAllies;
    public event Action<bool> BandSpeed;

    public BandData() {

        bandName = "bandName";
        bandLeader = null;
        bandMembers = new List<BandMemberData>();

        for ( int i = 0; i < maxNumMembers; i++ ) {

            bandMembers.Add( new BandMemberData() );
            bandMembers[i].ClearMember();
            bandMembers[i].SetPos( i + 1 );
        
        }

        isEmpty = true;
        memberCount = 0;
        bandSpeed = 0;

        bandMode = bandModes.Pelotón;

    }

    public int GetBandId( ) {

        return id;
    
    }

    public void SetBandId( int bandId ) {
    
        id = bandId;
    
    }

    public void SetLeader( Character leader ) {

        bandLeader = leader;

    }

    public Character GetLeader( ) {

        return bandLeader;

    }

    public void SetFactionName( string bandName ) {

        this.bandName = bandName;

    }

    public string GetFactionName( ) {

        return bandName;

    }

    public List<BandMemberData> GetMembers( ) {
    
        return bandMembers;
    
    }

    public bandModes GetBandMode( ) {

        return bandMode;
    
    }

    public bool IsBandEmpty( ) {
    
        return isEmpty;
    
    }

    public int GetMemberCount( ) {
    
        return memberCount;
    
    }

    public Character GetMember( int id ) {
    
        for(int i = 0; i < bandMembers.Count; i++) {

            if ( bandMembers[i].GetMember() == null) {

                Debug.Log("bandmember:    " + i);
            
            }

            if ( bandMembers[i].GetMember() != null && bandMembers[i].GetMember().GetId( ) == id ) {

                return bandMembers[i].GetMember();

            }
        
        }
        return null;
    
    }

    public void AddMember( Character character, string typeOfEntity ) {

        if ( bandMembers != null && !SearchMember(character) && bandMembers.Count <= maxNumMembers) {

            BandMemberData newMember = SearchEmptyMember();
            newMember.SetMember( character );
            newMember.SetSpeed( character.GetComponent<CharacterControllerAI>().GetSpeed() );

            memberCount++;

            character.SetBandId( id );
            character.SetInABand( true );
            character.tag = typeOfEntity;
            character.gameObject.GetComponent<CharacterControllerAI>().SetTarget( bandLeader.transform );
            character.gameObject.GetComponent<CharacterControllerAI>().SetBand( this );
            character.gameObject.GetComponent<CharacterControllerAI>().SetBandPos( newMember.GetMemberPos() );

            ChangeBandModeAction?.Invoke();

        }
        else {

            Debug.Log( "El tipo este ya es de este grupo." );

        }

    }

    public void RemoveMember( Character character, string typeOfEntity ) {

        if ( bandMembers != null && SearchMember( character ) ) {

            for ( int i = 0; i < bandMembers.Count; i++ ) {

                if ( bandMembers[i].GetMember() == character ) {

                    bandMembers[i].ClearMember();

                }
            
            }

            memberCount--;

            character.SetBandId( 0 );
            character.SetInABand( false );
            character.tag = typeOfEntity;
            character.gameObject.GetComponent<CharacterControllerAI>().SetTarget( character.transform );
            character.gameObject.GetComponent<CharacterControllerAI>().SetBand( null );
            character.gameObject.GetComponent<CharacterControllerAI>().SetBandPos( -1 );

        }
        else {

            Debug.Log( "El tipo este no está en este grupo." );

        }

    }

    public void ChangeBandMode( int index ) {

        bandMode = (bandModes)index;
        ChangeBandModeAction?.Invoke( );

    }

    public bool SearchMember( Character character ) {

        for ( int i = 0; i < bandMembers.Count; i++ ) {

            if ( bandMembers[i].GetMember() == character ) {

                return true;

            }
        
        }

        return false;
    
    }

    public BandMemberData GetBandMemberDataFromMember( Character member ) {

        for ( int i = 0; i < bandMembers.Count; i++ ) {

            if ( bandMembers[i].GetMember() == member ) {

                return bandMembers[i];

            }

        }

        return null;

    }

    public BandMemberData SearchEmptyMember( ) {

        for ( int i = 0; i < bandMembers.Count; i++ ) {

            if ( bandMembers[i].IsEmpty() ) {

                return bandMembers[i];
            
            }
        
        }

        return null;
    
    }

    public void UpdatePos( Character member, int pos ) {

        if ( SearchMember( member ) ) {

            GetBandMemberDataFromMember( member ).ClearMember();

        }

        for ( int i = 0; i < bandMembers.Count; i++ ) {

            if ( bandMembers[i].GetMemberPos() == pos ) {

                bandMembers[i].SetMember( member );

            }
        
        }
        member.GetComponent<CharacterControllerAI>().SetBandPos(pos);
        ChangeBandModeAction?.Invoke();

    }

    public void StandStillAllies( bool stand ) {
    
        StandStillAllAllies?.Invoke( stand );
    
    }

    // TODO
    public void SetBandSpeed( bool isBandSpeed ) {

        UpdateBandSpeed();
        BandSpeed?.Invoke( isBandSpeed );
    
    }

    public void Order( int orderIndex, bool activeClick ) {

        isBandSpeed = activeClick;

        switch ( orderIndex ) {

            case 1: // Stop or move allies

                StandStillAllies( activeClick );

                break;
            case 2: // BandSpeed

                SetBandSpeed( activeClick );

                break;
        
        }
    
    }

    public void UpdateBandSpeed( ) {

        float speed = 300.0f;

        for ( int i = 0; i < bandMembers.Count; i++ ) {

            if ( bandMembers[i].GetSpeed() < speed && !bandMembers[i].IsEmpty() ) {

                speed = bandMembers[i].GetSpeed();
                Debug.Log(speed);
            }
        
        }

        bandSpeed = speed;
    
    }

    public float GetBandSpeed( ) {

        return bandSpeed;
    
    }

    public bool isActiveBandSpeed( ) {

        return isBandSpeed;
    
    }

}
