<script>
    import {currentSong, isPlaying} from "../globals.js";
    import {loadSong, playSong, pauseSong, stopSong, setVolume} from "../audioController.js";
    
    export let song = null;

    let playing = false;
    isPlaying.subscribe(value => {
        playing = value;
    });
    
    let selectedSong = null;
    currentSong.subscribe(value => {
        selectedSong = value;
    });
    
    async function setCurrentSong() {
        if(!selectedSong) {
            currentSong.set(song);
            await loadSong();
            await playSong();
            await setVolume(0.25);
        }
        
        if(!playing && (song != selectedSong)) {
            currentSong.set(song);
            await stopSong();
            
            await loadSong();
            await playSong();
            await setVolume(0.25);
        }

        if(song != selectedSong) {
            currentSong.set(song);
            await stopSong();

            await loadSong();
            await playSong();
            await setVolume(0.25);
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
</style>

<div on:click={setCurrentSong}>
    {#if song}
        <td>{song.name}</td>
        <td>0</td>
        <td>{song.location}</td>
    {/if}
</div>
