import {
    currentDuration,
    currentPlaylist,
    currentSong,
    currentSongList,
    currentTime,
    currentVolume,
    isPlaying
} from "./globals.js";
import {writable} from "svelte/store";
import {Howl, Howler} from "howler";
import axios from "axios";
import {validate_each_keys} from "svelte/internal";

export const player = writable(null); //howl

let playerVar = null;
player.subscribe(value => {
    playerVar = value;
});

let selectedSong = null;
currentSong.subscribe(value => {
   selectedSong = value; 
});

let cVol = null;
currentVolume.subscribe(value => {
    cVol = value;
});

let selectedSongList = null;
currentSongList.subscribe(value => {
    selectedSongList = value;
});

let playing = false;
isPlaying.subscribe(value => {
    playing = value;
});

let selectedPlaylist = null;

// Subscribe to changes in the globalVariable
currentPlaylist.subscribe(value => {
    if(value != selectedPlaylist) {
        selectedPlaylist = value;
        //getPlaylistSongs();
    }
});

let cTime = null;
currentTime.subscribe(value => {
    cTime = value;
});

/*
async function getPlaylistSongs() {
    console.log("im the issue AUDIOCONTROLLER.SVELTE, GETPLAYLISTSONGS");
    currentSongList.set((await axios.post("http://localhost:5000/playlist/GET_PLAYLIST_SONGS", selectedPlaylist)).data);
}*/

async function setCurrentSong(song) {
    if(!selectedSong) {
        currentSong.set(song);
        await loadSong();
        await playSong();

        await setVolume(cVol);
    }

    if(!playing && (song != selectedSong)) {
        currentSong.set(song);
        await stopSong();

        await loadSong();
        await playSong();

        await setVolume(cVol);
    }

    if(song != selectedSong) {
        currentSong.set(song);
        await stopSong();

        await loadSong();
        await playSong();

        await setVolume(cVol);
    }
}

async function skipCurrent() {
    console.log(selectedSong.name);

    currentTime.set(0);

    let currentIndex = selectedSongList.findIndex(song => song.name === selectedSong.name);
    await setCurrentSong(selectedSongList[currentIndex + 1]);
}

let interval = null;

export async function loadSong() {
    if (playerVar) {
        playerVar.unload();
    }
    
    let howl = new Howl({
        src: ["http://localhost:5000/song/GET_SONG_STREAM?song=" + JSON.stringify(selectedSong)],
        autoplay: false,
        format: ['mp3'],
        html5: false,
        volume: cVol,
        onend: () => {
            clearInterval(interval);
            skipCurrent();

            howl.volume(cVol);
        },
        onplay: () => {
            howl.seek(cTime);

            howl.volume(cVol);
            
            interval = setInterval(() => {
                currentTime.set(howl.seek());
            }, 1000);

            currentDuration.set(howl.duration());
        },
        onstop: () => {
            clearInterval(interval);
            currentTime.set(0);

            howl.volume(cVol);
        },
        onload: () => {
            clearInterval(interval);
            currentTime.set(0);

            howl.volume(cVol);
        },
        onpause: () => {
            clearInterval(interval);

            howl.volume(cVol);
        }
    })
    
    player.set(howl);
}

export async function playSong() {
    isPlaying.set(true);
    player.update(howl => {
        if (howl) {
            howl.play();
            howl.seek(cTime);
        }
        return howl;
    });
}

export async function pauseSong() {
    isPlaying.set(false);
    player.update(howl => {
        if (howl) {
            howl.pause();
            console.log(howl.seek())
            currentTime.set(howl.seek());
        }
        
        return howl;
    });
}

export async function stopSong() {
    isPlaying.set(false);
    player.update(howl => {
        if (howl) {
            howl.stop();
        }
        return howl;
    });
}

export async function setVolume(volume) {
    currentVolume.set(volume);
    player.update(howl => {
        if (howl) {
            howl.volume(volume)
        }
        return howl;
    });
}

export async function unloadSong() {
    isPlaying.set(true);
    player.update(howl => {
        if (howl) {
            Howler.unload();
        }
        return howl;
    });
}