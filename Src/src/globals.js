import { writable } from "svelte/store";

export const currentPlaylist = writable(null); //string
export const currentSong = writable(null); //string
export const currentVolume = writable(0.01); //int
export const isPlaying = writable(false); // bool