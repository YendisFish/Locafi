﻿<script>
    import {currentPlaylist, currentSong, isPlaying, currentSongList, currentVolume} from "../globals.js";
    import {loadSong, playSong, pauseSong, stopSong, setVolume} from "../audioController.js";
    import axios from "axios";
    
    export let song = null;
    export let playlist = null;

    let playing = false;
    isPlaying.subscribe(value => {
        playing = value;
    });
    
    let selectedSong = null;
    currentSong.subscribe(value => {
        selectedSong = value;
    });

    let selectedSongList = null;
    currentSongList.subscribe(value => {
        selectedSongList = value;
    });

    let cVol = null;
    currentVolume.subscribe(value => {
        cVol = value;
    });

    async function getPlaylistSongs() {
        currentSongList.set((await axios.post("http://localhost:5000/playlist/GET_PLAYLIST_SONGS", playlist)).data);
    }
    
    async function setCurrentSong() {
        await getPlaylistSongs();

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
</script>

<style>
    td {
        padding: 8px 50px 8px 50px; /* Add padding to header cells */
        text-align: left; /* Align text to the left */
        /*padding-left: 50px;
        padding-right: 50px;*/
    }

    .song:nth-child(even) {
        background-color: #202123;
    }
</style>

<tr class="song" on:click={setCurrentSong}>
    {#if song}
        <td>{song.name}</td>
        <td>0</td>
        <td>{song.location}</td>
    {/if}
</tr>
