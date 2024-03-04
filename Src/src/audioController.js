import {currentSong, currentVolume, isPlaying} from "./globals.js";
import {writable} from "svelte/store";
import {Howl, Howler} from "howler";

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

export async function loadSong() {
    let howl = new Howl({
        src: ["http://localhost:5000/song/GET_SONG_STREAM?song=" + JSON.stringify(selectedSong)],
        autoplay: true,
        format: ['mp3'],
        enableRangeProcessing: false,
        volume: cVol
    })

    player.set(howl);
}

export async function playSong() {
    isPlaying.set(true);
    player.update(howl => {
        if (howl) {
            howl.play();
        }
        return howl;
    });
}

export async function pauseSong() {
    isPlaying.set(false);
    player.update(howl => {
        if (howl) {
            howl.pause();
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
    player.update(howl => {
        if (howl) {
            howl.volume(volume)
        }
        return howl;
    });
}