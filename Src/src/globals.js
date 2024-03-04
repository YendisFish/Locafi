import { writable } from "svelte/store";

export const currentPlaylist = writable(null); //string
export const currentSong = writable(null); //string
export const currentVolume = writable(0.25); //int
export const isPlaying = writable(false); // bool
export const currentSongList = writable(null); //song[]
export const currentTime = writable(0);
export const currentDuration = writable(0);

export function secondsToMinutes(seconds) {
    // Calculate minutes and seconds
    let minutes = Math.floor(seconds / 60);
    let remainingSeconds = Math.round(seconds % 60);

    // Pad with leading zero if necessary
    remainingSeconds = remainingSeconds < 10 ? "0" + remainingSeconds : remainingSeconds;

    // Return formatted string
    return `${minutes}:${remainingSeconds}`;
}