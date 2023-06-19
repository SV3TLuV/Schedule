//
//  SelectGroupView.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import SwiftUI

struct SelectGroupView: View {
    @ObservedObject private var groupModel = GroupViewModel()
    @State private var search: String = ""
    
    var body: some View {
        NavigationStack {
            List {
                ForEach(groupModel.groups) { group in
                    NavigationLink {
                        CurrentTimetableView(group: group)
                    } label: {
                        Text(group.name)
                    }
                }
                LoaderView(isFailed: groupModel.isRequestFailed, isHasMore: groupModel.isHasMore)
                    .onAppear(perform: loadMore)
                    .onTapGesture(perform: onTapLoadView)
            }
            .navigationTitle("Группы")
        }
        .searchable(text: $search, prompt: "Группа...")
        .onSubmit(of: .search, fetch)
        .onChange(of: search) {
            if (search.isEmpty) {
                fetch()
            }
        }
        .task {
            fetch()
        }
    }
    
    private func fetch() {
        groupModel.fetchGroups(search: search)
    }
    
    private func loadMore() {
        groupModel.loadMore(search: search)
    }
    
    private func onTapLoadView() {
        if groupModel.isRequestFailed {
            groupModel.isRequestFailed = false
            fetch()
        }
    }
}

#Preview {
    SelectGroupView()
}
