<script>
    import InputComponent from "./InputComponent.svelte";
    import axios from "axios";
    import { currentPlaylist } from "../globals.js";

    export let onDownload;
    
    let linkText = "";
    
    function onChangeInput(e) {
        linkText = e.target.value;
    }

    async function download() {
        let res = await axios.post("http://localhost:5000/DOWNLOAD", { link: linkText, playlist: selectedPlaylist });
        await onDownload();
    }

    let selectedPlaylist = null;

    // Subscribe to changes in the globalVariable
    currentPlaylist.subscribe(value => {
        selectedPlaylist = value;
    });
</script>

<style>

</style>

<div class="download-menu">
    <p style="color:white">Dowload</p>
    <InputComponent onChange="{onChangeInput}"/>
    <button on:click={download}>Download</button>
</div>