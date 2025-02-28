using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

EnsureDataLoaded();

string rootFolder = Path.GetDirectoryName(FilePath) + Path.DirectorySeparatorChar;
ThreadLocal<GlobalDecompileContext> DECOMPILE_CONTEXT = new ThreadLocal<GlobalDecompileContext>(
    () => new GlobalDecompileContext(Data, false)
);

// Export Project yyp
using (
    StreamWriter writer = new StreamWriter(rootFolder + Data.GeneralInfo.FileName.Content + ".yyp")
)
{
    writer.WriteLine("{");
    writer.WriteLine("  \"resourceType\": \"GMProject\",");
    writer.WriteLine("  \"resourceVersion\": \"1.7\",");
    writer.WriteLine("  \"name\": \"Match 3 Template\",");
    writer.WriteLine("  \"AudioGroups\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMAudioGroup\",\"resourceVersion\":\"1.3\",\"name\":\"audiogroup_default\",\"targets\":-1,},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"configs\": {");
    writer.WriteLine("    \"children\": [],");
    writer.WriteLine("    \"name\": \"Default\",");
    writer.WriteLine("  },");
    writer.WriteLine("  \"defaultScriptType\": 0,");
    writer.WriteLine("  \"Folders\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Chemins d'accès\",\"folderPath\":\"folders/Chemins d'accès.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Chronologies\",\"folderPath\":\"folders/Chronologies.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Courbes d’animation\",\"folderPath\":\"folders/Courbes d’animation.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Extensions\",\"folderPath\":\"folders/Extensions.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Fonts\",\"folderPath\":\"folders/Fonts.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Notes\",\"folderPath\":\"folders/Notes.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Objects\",\"folderPath\":\"folders/Objects.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Board and Pieces\",\"folderPath\":\"folders/Objects/Board and Pieces.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Controllers\",\"folderPath\":\"folders/Objects/Controllers.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Effects\",\"folderPath\":\"folders/Objects/Effects.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"HUD\",\"folderPath\":\"folders/Objects/HUD.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Main Menu\",\"folderPath\":\"folders/Objects/Main Menu.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Objets\",\"folderPath\":\"folders/Objets.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Particle Systems\",\"folderPath\":\"folders/Particle Systems.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Pièces\",\"folderPath\":\"folders/Pièces.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Polices\",\"folderPath\":\"folders/Polices.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Remarques\",\"folderPath\":\"folders/Remarques.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Rooms\",\"folderPath\":\"folders/Rooms.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Scripts\",\"folderPath\":\"folders/Scripts.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sequences\",\"folderPath\":\"folders/Sequences.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Séquences\",\"folderPath\":\"folders/Séquences.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Shaders\",\"folderPath\":\"folders/Shaders.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sons\",\"folderPath\":\"folders/Sons.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sounds\",\"folderPath\":\"folders/Sounds.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Music\",\"folderPath\":\"folders/Sounds/Music.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"SFX\",\"folderPath\":\"folders/Sounds/SFX.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Sprites\",\"folderPath\":\"folders/Sprites.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"BackgroundsAndTiles\",\"folderPath\":\"folders/Sprites/BackgroundsAndTiles.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"HUD\",\"folderPath\":\"folders/Sprites/HUD.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Pieces\",\"folderPath\":\"folders/Sprites/Pieces.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Tile sets\",\"folderPath\":\"folders/Tile sets.yy\",},"
    );
    writer.WriteLine(
        "    {\"resourceType\":\"GMFolder\",\"resourceVersion\":\"1.0\",\"name\":\"Tile Sets\",\"folderPath\":\"folders/Tile Sets.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"IncludedFiles\": [],");
    writer.WriteLine("  \"isEcma\": false,");
    writer.WriteLine("  \"LibraryEmitters\": [],");
    writer.WriteLine("  \"MetaData\": {");
    writer.WriteLine("    \"IDEVersion\": \"2023.11.1.129\",");
    writer.WriteLine("  },");
    writer.WriteLine("  \"resources\": [");
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_score_bar_full\",\"path\":\"sprites/spr_score_bar_full/spr_score_bar_full.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_play_field_dark\",\"path\":\"sprites/spr_play_field_dark/spr_play_field_dark.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_effect_game_won\",\"path\":\"objects/obj_effect_game_won/obj_effect_game_won.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_playfield_dark\",\"path\":\"objects/obj_playfield_dark/obj_playfield_dark.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_game_lose\",\"path\":\"sounds/snd_game_lose/snd_game_lose.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_special_token_whoosh\",\"path\":\"sounds/snd_special_token_whoosh/snd_special_token_whoosh.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_matching_pieces\",\"path\":\"sprites/spr_matching_pieces/spr_matching_pieces.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_effect_star\",\"path\":\"objects/obj_effect_star/obj_effect_star.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_playfield_light\",\"path\":\"objects/obj_playfield_light/obj_playfield_light.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"check_board_valid\",\"path\":\"scripts/check_board_valid/check_board_valid.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_moves_icon\",\"path\":\"sprites/spr_moves_icon/spr_moves_icon.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_score_background_vertical\",\"path\":\"sprites/spr_score_background_vertical/spr_score_background_vertical.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_coffee_drink\",\"path\":\"sounds/snd_powerup_coffee_drink/snd_powerup_coffee_drink.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_special_token_bomb\",\"path\":\"sounds/snd_special_token_bomb/snd_special_token_bomb.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_hud_moves_bg\",\"path\":\"sprites/spr_hud_moves_bg/spr_hud_moves_bg.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_button_quit\",\"path\":\"objects/obj_game_over_button_quit/obj_game_over_button_quit.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_donut_pop\",\"path\":\"sounds/snd_donut_pop/snd_donut_pop.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_text_popup\",\"path\":\"objects/obj_text_popup/obj_text_popup.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"check_on_startup\",\"path\":\"scripts/check_on_startup/check_on_startup.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_lose\",\"path\":\"objects/obj_game_over_lose/obj_game_over_lose.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_title_lose\",\"path\":\"objects/obj_game_over_title_lose/obj_game_over_title_lose.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_banner_uhoh\",\"path\":\"sprites/spr_banner_uhoh/spr_banner_uhoh.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_banner\",\"path\":\"sprites/spr_banner/spr_banner.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_controller\",\"path\":\"objects/obj_controller/obj_controller.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_no_valid_matches_title\",\"path\":\"objects/obj_no_valid_matches_title/obj_no_valid_matches_title.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_spatula\",\"path\":\"sounds/snd_powerup_spatula/snd_powerup_spatula.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_button_resume\",\"path\":\"objects/obj_button_resume/obj_button_resume.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"fnt_big_text\",\"path\":\"fonts/fnt_big_text/fnt_big_text.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_menu_button_quit\",\"path\":\"objects/obj_menu_button_quit/obj_menu_button_quit.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_button_next\",\"path\":\"objects/obj_game_over_button_next/obj_game_over_button_next.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_hud_star\",\"path\":\"sprites/spr_hud_star/spr_hud_star.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_title_win\",\"path\":\"objects/obj_game_over_title_win/obj_game_over_title_win.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_coffee_pour\",\"path\":\"sounds/snd_powerup_coffee_pour/snd_powerup_coffee_pour.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"fnt_small_text\",\"path\":\"fonts/fnt_small_text/fnt_small_text.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"tileset_board\",\"path\":\"tilesets/tileset_board/tileset_board.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_score\",\"path\":\"objects/obj_score/obj_score.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_game_over_next\",\"path\":\"sprites/spr_game_over_next/spr_game_over_next.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"objective_solve\",\"path\":\"scripts/objective_solve/objective_solve.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_match_big\",\"path\":\"sounds/snd_match_big/snd_match_big.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_objectives_background_vertical\",\"path\":\"sprites/spr_objectives_background_vertical/spr_objectives_background_vertical.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_menu_button_play\",\"path\":\"objects/obj_menu_button_play/obj_menu_button_play.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_game_win\",\"path\":\"sounds/snd_game_win/snd_game_win.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_win_template\",\"path\":\"sequences/seq_game_over_win_template/seq_game_over_win_template.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_circle_red\",\"path\":\"sprites/spr_button_circle_red/spr_button_circle_red.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_score_horizontal\",\"path\":\"objects/obj_score_horizontal/obj_score_horizontal.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"text_pop_up\",\"path\":\"scripts/text_pop_up/text_pop_up.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_invalid_match\",\"path\":\"sounds/snd_invalid_match/snd_invalid_match.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_home\",\"path\":\"sprites/spr_button_home/spr_button_home.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_wide_red\",\"path\":\"sprites/spr_button_wide_red/spr_button_wide_red.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_win\",\"path\":\"objects/obj_game_over_win/obj_game_over_win.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_effect_pieces\",\"path\":\"sprites/spr_effect_pieces/spr_effect_pieces.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_background\",\"path\":\"sprites/spr_background/spr_background.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_match_small\",\"path\":\"sounds/snd_match_small/snd_match_small.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_effect_matched\",\"path\":\"objects/obj_effect_matched/obj_effect_matched.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"draw_value_bordered\",\"path\":\"scripts/draw_value_bordered/draw_value_bordered.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_pause\",\"path\":\"objects/obj_game_pause/obj_game_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_star_award_complete\",\"path\":\"sounds/snd_star_award_complete/snd_star_award_complete.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_button_retry\",\"path\":\"objects/obj_game_over_button_retry/obj_game_over_button_retry.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_no_valid_matches\",\"path\":\"sequences/seq_no_valid_matches/seq_no_valid_matches.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_tileset_board\",\"path\":\"sprites/spr_tileset_board/spr_tileset_board.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_template_complete\",\"path\":\"sprites/spr_template_complete/spr_template_complete.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_objectives\",\"path\":\"objects/obj_objectives/obj_objectives.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_ui_click\",\"path\":\"sounds/snd_ui_click/snd_ui_click.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_banner_failed\",\"path\":\"sprites/spr_banner_failed/spr_banner_failed.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_menu_title\",\"path\":\"objects/obj_menu_title/obj_menu_title.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_star_award_in_game\",\"path\":\"sounds/snd_star_award_in_game/snd_star_award_in_game.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_retry\",\"path\":\"sprites/spr_button_retry/spr_button_retry.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"Documentation\",\"path\":\"notes/Documentation/Documentation.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_score_background_horizontal\",\"path\":\"sprites/spr_score_background_horizontal/spr_score_background_horizontal.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_play_field_light\",\"path\":\"sprites/spr_play_field_light/spr_play_field_light.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_button_pause\",\"path\":\"objects/obj_button_pause/obj_button_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_effect_star_particle\",\"path\":\"objects/obj_effect_star_particle/obj_effect_star_particle.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_objectives_horizontal\",\"path\":\"objects/obj_objectives_horizontal/obj_objectives_horizontal.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"fnt_medium_text\",\"path\":\"fonts/fnt_medium_text/fnt_medium_text.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"rm_level_3\",\"path\":\"rooms/rm_level_3/rm_level_3.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_score_bar_empty\",\"path\":\"sprites/spr_score_bar_empty/spr_score_bar_empty.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_donut_land\",\"path\":\"sounds/snd_donut_land/snd_donut_land.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_sprinkles\",\"path\":\"sounds/snd_powerup_sprinkles/snd_powerup_sprinkles.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"rm_menu\",\"path\":\"rooms/rm_menu/rm_menu.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_pause\",\"path\":\"sequences/seq_pause/seq_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_mute\",\"path\":\"sprites/spr_button_mute/spr_button_mute.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"rm_level_2\",\"path\":\"rooms/rm_level_2/rm_level_2.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_next\",\"path\":\"sprites/spr_button_next/spr_button_next.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_objective_tokens\",\"path\":\"sprites/spr_objective_tokens/spr_objective_tokens.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"screen_resize\",\"path\":\"scripts/screen_resize/screen_resize.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_wide_green\",\"path\":\"sprites/spr_button_wide_green/spr_button_wide_green.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_banner_complete\",\"path\":\"sprites/spr_banner_complete/spr_banner_complete.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_circle_green\",\"path\":\"sprites/spr_button_circle_green/spr_button_circle_green.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_game_over_template_end\",\"path\":\"objects/obj_game_over_template_end/obj_game_over_template_end.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_decal\",\"path\":\"sprites/spr_decal/spr_decal.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_matching_piece\",\"path\":\"objects/obj_matching_piece/obj_matching_piece.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_lose\",\"path\":\"sequences/seq_game_over_lose/seq_game_over_lose.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"fnt_score\",\"path\":\"fonts/fnt_score/fnt_score.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_game_over_window\",\"path\":\"sprites/spr_game_over_window/spr_game_over_window.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"seq_game_over_win_level\",\"path\":\"sequences/seq_game_over_win_level/seq_game_over_win_level.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_icing\",\"path\":\"sounds/snd_powerup_icing/snd_powerup_icing.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_powerup_selection\",\"path\":\"sounds/snd_powerup_selection/snd_powerup_selection.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_objectives_background_horizontal\",\"path\":\"sprites/spr_objectives_background_horizontal/spr_objectives_background_horizontal.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_logo\",\"path\":\"sprites/spr_logo/spr_logo.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_piece_swap\",\"path\":\"sounds/snd_piece_swap/snd_piece_swap.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_no_valid_matches\",\"path\":\"objects/obj_no_valid_matches/obj_no_valid_matches.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_playfield_parent\",\"path\":\"objects/obj_playfield_parent/obj_playfield_parent.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_banner_paused\",\"path\":\"sprites/spr_banner_paused/spr_banner_paused.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_button_audio\",\"path\":\"objects/obj_button_audio/obj_button_audio.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"fnt_main\",\"path\":\"fonts/fnt_main/fnt_main.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_ui_close\",\"path\":\"sounds/snd_ui_close/snd_ui_close.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_music_game\",\"path\":\"sounds/snd_music_game/snd_music_game.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"snd_music_menu\",\"path\":\"sounds/snd_music_menu/snd_music_menu.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_tick\",\"path\":\"sprites/spr_tick/spr_tick.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"obj_banner_pause\",\"path\":\"objects/obj_banner_pause/obj_banner_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"spr_button_pause\",\"path\":\"sprites/spr_button_pause/spr_button_pause.yy\",},},"
    );
    writer.WriteLine(
        "    {\"id\":{\"name\":\"rm_level_1\",\"path\":\"rooms/rm_level_1/rm_level_1.yy\",},},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"RoomOrderNodes\": [");
    writer.WriteLine(
        "    {\"roomId\":{\"name\":\"rm_menu\",\"path\":\"rooms/rm_menu/rm_menu.yy\",},},"
    );
    writer.WriteLine(
        "    {\"roomId\":{\"name\":\"rm_level_1\",\"path\":\"rooms/rm_level_1/rm_level_1.yy\",},},"
    );
    writer.WriteLine(
        "    {\"roomId\":{\"name\":\"rm_level_2\",\"path\":\"rooms/rm_level_2/rm_level_2.yy\",},},"
    );
    writer.WriteLine(
        "    {\"roomId\":{\"name\":\"rm_level_3\",\"path\":\"rooms/rm_level_3/rm_level_3.yy\",},},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"templateType\": \"game\",");
    writer.WriteLine("  \"TextureGroups\": [");
    writer.WriteLine(
        "    {\"resourceType\":\"GMTextureGroup\",\"resourceVersion\":\"1.3\",\"name\":\"Default\",\"autocrop\":true,\"border\":2,\"compressFormat\":\"bz2\",\"directory\":\"\",\"groupParent\":null,\"isScaled\":true,\"loadType\":\"default\",\"mipsToGenerate\":0,\"targets\":-1,},"
    );
    writer.WriteLine("  ],");
    writer.Write("}");
}

// Export Project Resource Order
using (
    StreamWriter writer = new StreamWriter(
        rootFolder + Data.GeneralInfo.FileName.Content + ".resource_order"
    )
)
{
    writer.WriteLine("{");
    writer.WriteLine("  \"FolderOrderSettings\": [");
    writer.WriteLine(
        "    {\"name\":\"Chemins d'accès\",\"order\":3,\"path\":\"folders/Chemins d'accès.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Chronologies\",\"order\":6,\"path\":\"folders/Chronologies.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Courbes d’animation\",\"order\":10,\"path\":\"folders/Courbes d’animation.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Extensions\",\"order\":12,\"path\":\"folders/Extensions.yy\",},"
    );
    writer.WriteLine("    {\"name\":\"Objets\",\"order\":7,\"path\":\"folders/Objets.yy\",},");
    writer.WriteLine(
        "    {\"name\":\"Particle Systems\",\"order\":13,\"path\":\"folders/Particle Systems.yy\",},"
    );
    writer.WriteLine("    {\"name\":\"Pièces\",\"order\":8,\"path\":\"folders/Pièces.yy\",},");
    writer.WriteLine("    {\"name\":\"Polices\",\"order\":5,\"path\":\"folders/Polices.yy\",},");
    writer.WriteLine(
        "    {\"name\":\"Remarques\",\"order\":11,\"path\":\"folders/Remarques.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"Séquences\",\"order\":9,\"path\":\"folders/Séquences.yy\",},"
    );
    writer.WriteLine("    {\"name\":\"Shaders\",\"order\":4,\"path\":\"folders/Shaders.yy\",},");
    writer.WriteLine("    {\"name\":\"Sons\",\"order\":2,\"path\":\"folders/Sons.yy\",},");
    writer.WriteLine(
        "    {\"name\":\"Tile sets\",\"order\":1,\"path\":\"folders/Tile sets.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.WriteLine("  \"ResourceOrderSettings\": [");
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_win_template\",\"order\":4,\"path\":\"sequences/seq_game_over_win_template/seq_game_over_win_template.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_no_valid_matches\",\"order\":1,\"path\":\"sequences/seq_no_valid_matches/seq_no_valid_matches.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_lose\",\"order\":2,\"path\":\"sequences/seq_game_over_lose/seq_game_over_lose.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"seq_game_over_win_level\",\"order\":3,\"path\":\"sequences/seq_game_over_win_level/seq_game_over_win_level.yy\",},"
    );
    writer.WriteLine(
        "    {\"name\":\"obj_banner_pause\",\"order\":1,\"path\":\"objects/obj_banner_pause/obj_banner_pause.yy\",},"
    );
    writer.WriteLine("  ],");
    writer.Write("}");
}

ScriptMessage("Export Complete.\n\nLocation: " + rootFolder);
