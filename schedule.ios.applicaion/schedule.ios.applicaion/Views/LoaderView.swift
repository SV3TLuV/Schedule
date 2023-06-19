//
//  LoaderView.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import SwiftUI

struct LoaderView: View {
    let isFailed: Bool
    let isHasMore: Bool
    
    var body: some View {
        if isHasMore {
            Text(isFailed ? "Ошибка." : "Загрузка...")
                .foregroundColor(isFailed ? .red : .secondary)
        } else {
            EmptyView()
        }
    }
}

#Preview {
    LoaderView(isFailed: false, isHasMore: true)
}
