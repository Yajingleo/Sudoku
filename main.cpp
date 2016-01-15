//
//  main.cpp
//  Sudoku
//
//  Created by YAJING LIU on 1/14/16.
//  Copyright © 2016 YAJING LIU. All rights reserved.
//

#include <iostream>
#include <vector>
#include <string>
using namespace std;

class Solution {
public:
    void solveSudoku(vector<vector<char>>& board) {
        vector<vector<vector<char>>> results;
        dfs(board, 0, 0, results);
//        for (auto Board: results){
//            for (auto row: Board){
//                for (char a: row){
//                    cout << a;
//                }
//                cout <<endl;
//            }
//                
//            cout << endl;
//        }
        
    }
    
    void dfs(vector<vector<char>>& board, int i, int j, vector<vector<vector<char>>>& results){
        if (i==9) {
            results.push_back(board);
            for (auto row: board){
                for (char a: row){
                    cout << a;
                }
                cout <<endl;
            }
            
            cout << endl;

            return;
        }
        
        int i1,j1;
        if (j==8) {
            i1=i+1;
            j1=0;
        }
        else {
            i1=i;
            j1=j+1;
        }
            
        if (board[i][j]!='.'){
            dfs(board, i1, j1, results);
        }
        else {
            for (int k=0; k<9; k++){
                board[i][j]='1'+k;
                if (checkat(board, i, j)) dfs(board, i1, j1, results);
                board[i][j]='.';
            }
        }
    }
    
    bool checkat(vector<vector<char>>& board, int i, int j){
        for (int x=0; x<9; x++){
            if (i!=x && board[i][j]==board[x][j])return false;
        }
        for (int y=0; y<9; y++){
            if (j!=y && board[i][j]== board[i][y]) return false;
        }
        for (int x=3*(i/3); x<3*(i/3)+3; x++){
            for (int y=3*(j/3); y<3*(j/3)+3; y++){
                if ( (i!=x || j!=y) && board[i][j]==board[x][y]) return false;
            }
        }
        return true;
    }
    
    
};

int main(int argc, const char * argv[]) {
    vector<string> B ({"123456789",".........",".........",".........",".........",".........",".........",".........","........."});
    vector<vector<char>> board;
    for (int i=0; i<9; i++) {
        board.push_back(vector<char> (B[i].begin(), B[i].end()));
        cout << B[i] <<endl;
    }
    Solution S;
    S.solveSudoku(board);
    
    return 0;
}
